namespace Authentication.Infrastructure.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = default!;
    public string JwtId { get; set; } = default!;
    public bool IsRevoked { get; set; }
    public DateTime DateAdded { get; set; } = default!;
    public DateTime DateExpire { get; set; } = default!;
    public string UserId { get; set; } = default!;

    public AppUser User { get; set; } = default!;
}
