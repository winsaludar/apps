namespace Authentication.Infrastructure.Settings;

public record SmtpSettings
{
    public string Host { get; init; } = default!;
    public int Port { get; init; } = default!;
    public string Login { get; init; } = default!;
    public string Password { get; init; } = default!;
    public bool EnableSsl { get; init; } = default!;
    public string FromEmail { get; set; } = default!;
    public string FromName { get; set; } = default!;
}
