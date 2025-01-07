namespace Authentication.Infrastructure.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public IEnumerable<RefreshToken> RefreshTokens { get; set; } = default!;
}
