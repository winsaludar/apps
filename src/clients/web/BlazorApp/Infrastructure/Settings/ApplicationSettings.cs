namespace BlazorApp.Infrastructure.Settings;

public record ApplicationSettings
{
    public string TokenName { get; init; } = default!;
    public string RefreshTokenName { get; init; } = default!;
    public string FriendlyError { get; set; } = default!;
}
