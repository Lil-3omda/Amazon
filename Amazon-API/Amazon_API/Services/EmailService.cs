using Amazon_API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    private readonly SendGridClient _sendGridClient;
    private readonly EmailAddress _fromAddress;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _logger = logger;

        var apiKey = configuration["SendGrid:ApiKey"];
        var senderEmail = configuration["SendGrid:SenderEmail"];
        var senderName = configuration["SendGrid:SenderName"] ?? "No-Reply";

        if (string.IsNullOrEmpty(apiKey))
            throw new ArgumentException("SendGrid API Key is missing in configuration.");

        if (string.IsNullOrEmpty(senderEmail))
            throw new ArgumentException("SendGrid Sender Email is missing in configuration.");

        _sendGridClient = new SendGridClient(apiKey);
        _fromAddress = new EmailAddress(senderEmail, senderName);
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("Recipient email is required", nameof(email));
        if (string.IsNullOrEmpty(subject))
            throw new ArgumentException("Email subject is required", nameof(subject));
        if (string.IsNullOrEmpty(htmlMessage))
            throw new ArgumentException("Email message is required", nameof(htmlMessage));

        var toAddress = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(
            from: _fromAddress,
            to: toAddress,
            subject: subject,
            plainTextContent: null,
            htmlContent: htmlMessage
        );

        try
        {
            var response = await _sendGridClient.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Body.ReadAsStringAsync();
                _logger.LogError("Failed to send email to {Email}. StatusCode: {StatusCode}. Response: {Response}",
                    email, response.StatusCode, error);
            }
            else
            {
                _logger.LogInformation("Email sent successfully to {Email}", email);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception while sending email to {Email}", email);
        }
    }
}
