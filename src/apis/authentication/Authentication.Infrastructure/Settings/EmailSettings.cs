namespace Authentication.Infrastructure.Settings;

public record EmailSettings
{
    public string ClientBaseUrl { get; init; } = default!;
    public string ClientConfirmEmailPath { get; init; } = default!;
    public string ClientResetPasswordPath { get; init; } = default!;

    public string EmailConfirmationSubject { get; init; } = default!;
    public int EmailConfirmationExpirationInMinutes { get; init; } = default!;
    public string EmailConfirmationTemplateName { get; init; } = default!;

    public string ForgotPasswordSubject { get; init; } = default!;
    public int ForgotPasswordExpirationInMinutes { get; init; } = default!;
    public string ForgotPasswordTemplateName { get; init; } = default!;
}
