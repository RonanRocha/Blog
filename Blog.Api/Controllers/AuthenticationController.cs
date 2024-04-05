using Microsoft.AspNetCore.Mvc;
using Blog.Domain.Account.Services;
using Blog.Domain.Account.Entities;
using Blog.Domain.Core.Entities;
using Blog.Application.Account.Commands;

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

            if (String.IsNullOrEmpty(command.Password) || String.IsNullOrEmpty(command.Email))
                return BadRequest("Failed login attempt");

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
            if (String.IsNullOrEmpty(command.Password) || String.IsNullOrEmpty(command.Email))
                return BadRequest("Failed register attempt");

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
        public async Task<IActionResult> LoginMfa(string code, string email)
        {
            if (String.IsNullOrEmpty(code))
                return BadRequest("Failed to login");

            var result = await _authenticationService.LoginMfaAsync(code, "Email");

            if (!result.Succeeded) return BadRequest("Failed login attenpt");

            User user = await _authenticationService.GetUserAsync(email);
            UserToken userToken = await _tokenService.GenerateTokenAsync(user);
            return Ok(userToken);

        }
    }
}
