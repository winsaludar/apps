namespace Authentication.Infrastructure.Configurations;

public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasMany(x => x.RefreshTokens)
            .WithOne(y => y.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
