namespace Authentication.Infrastructure.Settings;

public record JwtSettings
{
    public string Secret { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string Issuer { get; init; } = default!;
    public int ExpirationInMinutes { get; init; } = default!;
    public int RefreshTokenExpirationInMonths { get; init; } = default!;
}

