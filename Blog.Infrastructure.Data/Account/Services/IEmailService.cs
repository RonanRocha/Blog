using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Data.Account.Services
{
    public interface IEmailService
    {
        void Send(string email, string subject, string body);
    }
}
