using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Account.Commands
{
    public class RegisterCommand
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
