namespace Authentication.API.Registrars;

public class AuthenticationRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        string jwtSecret = configuration["JWT:Secret"] ?? string.Empty;
        string jwtIssuer = configuration["JWT:Issuer"] ?? string.Empty;
        string jwtAudience = configuration["JWT:Audience"] ?? string.Empty;

        // Configure token validation parameters 
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        services.AddSingleton(tokenValidationParameters);

        // Configure Identity
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();

        // Configure JWT Authentication Scheme
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = tokenValidationParameters;
        });
    }
}
