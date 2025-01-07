namespace Authentication.API.Registrars;

public class ServiceRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        // Add settings here so we can use it anywhere...
        services.AddSettings<SmtpSettings>(nameof(SmtpSettings), configuration);
        services.AddSettings<EmailSettings>(nameof(EmailSettings), configuration);

        // Add custom services, repositories, and other dependencies here...
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmailService, EmailService>();
    }
}
