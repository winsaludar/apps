namespace Budget.API.Registrars;

public class AuthenticationRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        // Bind jwtSettings so we can use it anywhere
        JwtClientSettings jwtSettings = new();
        configuration.GetSection(nameof(JwtClientSettings)).Bind(jwtSettings);
        services.AddSingleton(jwtSettings);

        // Configure token validation parameters 
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),            
            ClockSkew = TimeSpan.FromMinutes(1),
        };
        services.AddSingleton(tokenValidationParameters);

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

        services.AddAuthorization();
        services.AddHttpContextAccessor();
    }
}
