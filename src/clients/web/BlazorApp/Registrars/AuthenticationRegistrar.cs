namespace BlazorApp.Registrars;

public class AuthenticationRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        // Bind jwtSettings so we can use it anywhere
        JwtClientSettings jwtClientSettings = new();
        configuration.GetSection(nameof(JwtClientSettings)).Bind(jwtClientSettings);
        services.AddSingleton(jwtClientSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = jwtClientSettings.Authority;
            options.Audience = jwtClientSettings.Audience;
            options.RequireHttpsMetadata = false;
        });

        //services.AddScoped<JwtAuthenticationStateProvider>();
    }
}
