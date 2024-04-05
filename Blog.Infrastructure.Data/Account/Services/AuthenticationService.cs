using Blog.Domain.Core.Entities;
using Blog.Domain.Account.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Blog.Domain.Account.Entities;

namespace Blog.Infrastructure.Data.Account.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IEmailService _emailService;

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<SignInResult> AuthenticateAsync(string email, string password)
        {

            SignInResult signInResult;

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null) throw new Exception("Something is wrong");

                if (user.TwoFactorEnabled)
                {
                    await LogoutAsync();

                    signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                    if (signInResult.RequiresTwoFactor)
                    {
                        var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                        _emailService.Send(user.Email, "Cofirm code", $" this is your token {token}");
                        return signInResult;
                    }

                    return signInResult;
                }

                return await _signInManager.PasswordSignInAsync(email, password, false, false);

            }
            catch (Exception ex)
            {
                return new LoginResult(false);
            }

        }

        public async Task<IdentityResult> ConfirmEmailAsync(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) throw new Exception("Something is wrong");

            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginMfaAsync(string code, string provider)
        {
            
           return  await _signInManager.TwoFactorSignInAsync(provider, code, false, false);
            
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUserAsync(string name, string email, string password)
        {
            try
            {
                var applicationUser = new User
                {
                    Email = email,
                    UserName = email,
                    Name = name
                };

                var result = await _userManager.CreateAsync(applicationUser, password);

                if (result.Succeeded)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("Permission", "CanRead"));
                    claims.Add(new Claim("Permission", "CanWrite"));

                    await _userManager.AddToRoleAsync(applicationUser, "User");
                    await _userManager.AddClaimsAsync(applicationUser, claims);
                    await _signInManager.SignInAsync(applicationUser, isPersistent: false);

                    var user = await _userManager.FindByEmailAsync(applicationUser.Email);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    _emailService.Send(user.Email, "Cofirm email", $" this is your token {token}");

                }

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
