using Blog.Domain.Account.Services;
using Blog.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Blog.Infrastructure.Data.Account.Services
{
    public class SeedAccountService : ISeedAccountService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;



        public SeedAccountService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                };

                IdentityResult result = _roleManager.CreateAsync(role).Result;

            }

            if (!_roleManager.RoleExistsAsync("Publisher").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Publisher",
                    NormalizedName = "PUBLISHER"
                };

                IdentityResult result = _roleManager.CreateAsync(role).Result;

            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                IdentityResult result = _roleManager.CreateAsync(role).Result;

            }
        }

        public void SeedUsers()
        {

            var admin = _configuration["DefaultUsers:Admin:Name"];
            var publisher = _configuration["DefaultUsers:Publisher"];
            var defaultUser = _configuration["DefaultUsers:User"];


            if (_userManager.FindByEmailAsync(_configuration["DefaultUsers:Admin:Email"]).Result == null)
            {
                User user = new User
                {
                    UserName = _configuration["DefaultUsers:Admin:Email"],
                    Name = "Admin User",
                    Email = _configuration["DefaultUsers:Admin:Email"],
                    EmailConfirmed = true,
                    NormalizedEmail = _configuration["DefaultUsers:Admin:Email"].ToUpper(),
                    NormalizedUserName = _configuration["DefaultUsers:Admin:Email"].ToUpper(),
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                IdentityResult result = _userManager.CreateAsync(user, _configuration["DefaultUsers:Admin:Password"]).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }

            }

            if (_userManager.FindByEmailAsync(_configuration["DefaultUsers:Publisher:Email"]).Result == null)
            {
                User user = new User
                {
                    UserName = _configuration["DefaultUsers:Publisher:Email"],
                    Name = "Publisher User",
                    Email = _configuration["DefaultUsers:Publisher:Email"],
                    EmailConfirmed = true,
                    NormalizedEmail = _configuration["DefaultUsers:Publisher:Email"].ToUpper(),
                    NormalizedUserName = _configuration["DefaultUsers:Publisher:Email"].ToUpper(),
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                IdentityResult result = _userManager.CreateAsync(user, _configuration["DefaultUsers:Publisher:Password"]).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Publisher").Wait();
                }

            }

            if (_userManager.FindByEmailAsync(_configuration["DefaultUsers:User:Email"]).Result == null)
            {
                User user = new User
                {
                    UserName = _configuration["DefaultUsers:User:Email"],
                    Name = "Default User",
                    Email = _configuration["DefaultUsers:User:Email"],
                    EmailConfirmed = true,
                    NormalizedEmail = _configuration["DefaultUsers:User:Email"].ToUpper(),
                    NormalizedUserName = _configuration["DefaultUsers:User:Email"].ToUpper(),
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                IdentityResult result = _userManager.CreateAsync(user, _configuration["DefaultUsers:User:Password"]).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "User").Wait();
                }

            }
        }
    }
}
