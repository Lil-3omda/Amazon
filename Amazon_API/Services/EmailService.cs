using Amazon_API.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Amazon_API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var email = config["Email:Username"];
            var password = config["Email:Password"];
            var host = config["Email:Host"];
            var port = int.Parse(config["Email:Port"]);

            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true
            };

            var mail = new MailMessage(email, toEmail, subject, message);
            await client.SendMailAsync(mail);
        }
    }
}
