using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Data.Account.Services
{
    public class EmailService : IEmailService
    {
        
        public void Send(string email, string subject, string body)
        {
            using SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 1025;
            smtpClient.Host = "localhost";

            smtpClient.Send(new MailMessage(

                from: "blog@localhost",
                to: email,
                subject: subject,
                body: body
            ));
        }
    }
}
