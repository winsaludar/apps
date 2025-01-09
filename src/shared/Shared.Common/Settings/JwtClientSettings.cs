namespace Shared.Common.Settings;

public record JwtClientSettings
{
    public string Authority { get; set; } = default!;
    public string Secret { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string Issuer { get; init; } = default!;
}