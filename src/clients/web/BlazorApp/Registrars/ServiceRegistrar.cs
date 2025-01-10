namespace BlazorApp.Registrars;

public class ServiceRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings<AuthenticationApiSettings>(nameof(AuthenticationApiSettings), configuration);
        services.AddSettings<ApplicationSettings>(nameof(ApplicationSettings), configuration);

        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

        services.AddHttpClient<IAuthenticationHttpClient, AuthenticationHttpClient>();
    }
}
