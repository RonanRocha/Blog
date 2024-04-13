using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Account.Commands
{
    public class LoginCommand
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
