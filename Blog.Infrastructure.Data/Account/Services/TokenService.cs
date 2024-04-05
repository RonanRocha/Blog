using Blog.Domain.Account.Entities;
using Blog.Domain.Core.Entities;
using Blog.Domain.Account.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Infrastructure.Data.Account.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService _authenticationService;

        public TokenService(IConfiguration configuration, IAuthenticationService authenticationService)
        {
            _configuration = configuration;
            _authenticationService = authenticationService;
        }

        public async Task<UserToken> GenerateTokenAsync(User user)
        {
            IList<Claim> claims = await _authenticationService.GetClaimsAsync(user);
            IList<string> roles = await _authenticationService.GetRolesAsync(user);
            
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("UserId", user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            foreach(var role in roles) 
            { 
                claims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Expiration = expiration,
            };
        }
    }
}
