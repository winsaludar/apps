namespace BlazorApp.Registrars;

public class ServiceRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings<AuthenticationApiSettings>(nameof(AuthenticationApiSettings), configuration);
        services.AddScoped<IAuthenticationHttpClient, AuthenticationHttpClient>();
    }
}
