using Amazon_API.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

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

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Amazon App", email));
            mimeMessage.To.Add(MailboxAddress.Parse(toEmail));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain") { Text = message };

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(email, password);
                await smtp.SendAsync(mimeMessage);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }
    }
}