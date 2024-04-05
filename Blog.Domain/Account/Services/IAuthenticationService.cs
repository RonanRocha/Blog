using Blog.Domain.Account.Entities;
using Blog.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.Domain.Account.Services
{
    public interface IAuthenticationService
    {
        Task<SignInResult> AuthenticateAsync(string email, string password);
        Task<bool> RegisterUserAsync(string name, string email, string password);
        Task<IdentityResult> ConfirmEmailAsync(string token, string email);
        Task<User> GetUserAsync(string email);  
        Task<IList<Claim>> GetClaimsAsync(User user);
        Task<IList<string>> GetRolesAsync(User user);
        Task<SignInResult> LoginMfaAsync(string code, string provider);
        Task LogoutAsync();
    }
}
