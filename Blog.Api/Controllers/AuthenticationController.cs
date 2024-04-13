using Microsoft.AspNetCore.Mvc;
using Blog.Domain.Account.Services;
using Blog.Domain.Account.Entities;
using Blog.Domain.Core.Entities;
using Blog.Application.Account.Commands;
using System.Security.Policy;
using Hangfire;
using Microsoft.AspNetCore.Identity;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IAuthenticationService authenticationService, ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value?.Errors));

            Microsoft.AspNetCore.Identity.SignInResult result = await _authenticationService.AuthenticateAsync(command.Email, command.Password);

            if (result.RequiresTwoFactor)
            {
                return Ok("We sent confirmation in your e-mail");
            }

            if (!result.Succeeded) return BadRequest("Failed login attempt");

            if (result.Succeeded)
            {
                User user = await _authenticationService.GetUserAsync(command.Email);
                UserToken userToken = await _tokenService.GenerateTokenAsync(user);
                return Ok(userToken);
            }

            return BadRequest("Failed login attempt");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value?.Errors));

            var result = await _authenticationService.RegisterUserAsync(command.Name, command.Email, command.Password);

            if (!result) return BadRequest("Failed register attempt");

            return Ok("Register successfuly");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            if (String.IsNullOrEmpty(token) || String.IsNullOrEmpty(email))
                return BadRequest("Failed to confirm email");

            var result = await _authenticationService.ConfirmEmailAsync(token, email);

            if (result.Succeeded) return Ok("Email confirmed successfuly");

            return BadRequest("Failed to confirm email");

        }

        [HttpPost("LoginMfa")]
        public async Task<IActionResult> LoginMfa([FromBody] LoginMfaCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.LoginMfaAsync(command.Code, "Email");

                if (!result.Succeeded) return BadRequest("Failed login attenpt");

                User user = await _authenticationService.GetUserAsync(command.Email);
                UserToken userToken = await _tokenService.GenerateTokenAsync(user);
                return Ok(userToken);
            }

            return BadRequest("Failed login attenpt");

        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordCommand command)
        {

            if (ModelState.IsValid)
            {
                BackgroundJob.Enqueue("resetpasswordqueue", () =>
                         _authenticationService.ResetPasswordAsync(command.Email)
                );
            }

            return Ok("Instructions to change password as sent to email");
        }

        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            return Ok(new { token = token, email = email });
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ResetPasswordCommand command)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value?.Errors));

            IdentityResult result = await _authenticationService.ChangePasswordAsync(command.Email, command.Password, command.Token);
            if (!result.Succeeded) return BadRequest("Something is wrong");
            return Ok("Password changed successfuly");

        }
    }
}