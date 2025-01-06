namespace Authentication.Infrastructure.Configurations;

public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(nameof(RefreshToken));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Token).IsRequired();
        builder.Property(x => x.JwtId).IsRequired();
        builder.Property(x => x.IsRevoked).IsRequired();
        builder.Property(x => x.DateAdded).IsRequired();
        builder.Property(x => x.DateExpire).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
    }
}
