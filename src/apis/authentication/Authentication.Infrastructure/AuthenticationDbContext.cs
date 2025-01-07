namespace Authentication.Infrastructure;

public class AuthenticationDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Entities.RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly);
    }
}
