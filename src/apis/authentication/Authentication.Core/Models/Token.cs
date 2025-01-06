namespace Authentication.Core.Models;

public class Token
{
    private Token() { }

    public static Token Create(string value, string refreshToken, DateTime expiresAt)
    {
        return new Token { Value = value, RefreshToken = refreshToken, ExpiresAt = expiresAt };
    }

    public string Value { get; private set; } = default!;
    public string RefreshToken { get; private set; } = default!;
    public DateTime ExpiresAt { get; private set; }
}