namespace Authentication.Core.Models;

public class RefreshToken
{
    private RefreshToken() { }

    public static RefreshToken Create(string token, string jwtId, bool isRevoked, string userId, DateTime dateAdded, DateTime dateExpire, Guid? id = null)
    {
        return new RefreshToken 
        {
            Id = id ?? Guid.NewGuid(),
            Token = token,
            JwtId = jwtId,
            IsRevoked = isRevoked,
            UserId = userId,
            DateAdded = dateAdded,
            DateExpire = dateExpire
        };
    }

    public Guid Id { get; private set; }
    public string Token { get; private set; } = default!;
    public string JwtId { get; private set; } = default!;
    public bool IsRevoked { get; private set; }
    public DateTime DateAdded { get; private set; }
    public DateTime DateExpire { get; private set; }
    public string UserId { get; private set; } = default!;
}
