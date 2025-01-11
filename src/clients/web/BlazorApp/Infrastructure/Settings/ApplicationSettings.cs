namespace BlazorApp.Infrastructure.Settings;

public record ApplicationSettings
{
    public string TokenName { get; init; } = default!;
    public string RefreshTokenName { get; init; } = default!;
    public string ContactUsEmail { get; init; } = default!;
}
