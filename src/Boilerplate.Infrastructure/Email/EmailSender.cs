using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Boilerplate.Infrastructure.Email;

public class EmailSender(ILogger<EmailSender> logger, IOptions<EmailSettings> emailSettings) : IEmailSender
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        logger.LogInformation("Sending email to |{to}| from |{from}| with subject |{subject}|",
            email, _emailSettings.FromEmail, subject);

        try
        {
            MimeMessage mailMessage = new();

            mailMessage.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
            mailMessage.To.Add(new MailboxAddress(email, email));

            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

            SmtpClient client = new();
            await client.ConnectAsync(_emailSettings.SmtpHost, _emailSettings.SmtpPort);
            await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.FromPassword);

            await client.SendAsync(mailMessage);

            await client.DisconnectAsync(true);

            logger.LogInformation("Email sent to |{to}| from |{from}| with subject |{subject}|",
                email, _emailSettings.FromEmail, subject);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while sending email: {message}", ex.Message);
            throw;
        }
    }
}