namespace Boilerplate.Infrastructure.Email;

public record EmailSettings
{
    public required bool UseFakeClient { get; init; }
    public required string SmtpHost { get; init; }
    public required int SmtpPort { get; init; }
    public required string FromName { get; init; }
    public required string FromEmail { get; init; }
    public required string FromPassword { get; init; }
}