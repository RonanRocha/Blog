using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Account.Commands
{
    public class LoginMfaCommand
    { 


        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
