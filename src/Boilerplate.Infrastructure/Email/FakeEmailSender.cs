using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure.Email;

public class FakeEmailSender(ILogger<FakeEmailSender> logger) : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        logger.LogInformation("Sending email to |{email}| with subject |{subject}| and body |{htmlMessage}|",
            email, subject, htmlMessage);
        return Task.CompletedTask;
    }
}