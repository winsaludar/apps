namespace Shared.Http.Settings;

public record AuthenticationApiSettings
{
    public string BaseUrl { get; init; } = default!;
    public string LoginRoute { get; init; } = default!;
    public string RegisterRoute { get; init; } = default!;
    public string ConfirmEmailRoute { get; init; } = default!;
    public string ResendEmailConfirmationRoute { get; init; } = default!;
    public string ForgotPasswordRoute { get; set; } = default!;
    public string ResetPasswordRoute { get; set; } = default!;
}

