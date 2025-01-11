namespace BlazorApp.Registrars;

public class ServiceRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings<AuthenticationApiSettings>(nameof(AuthenticationApiSettings), configuration);
        services.AddSettings<ApplicationSettings>(nameof(ApplicationSettings), configuration);

        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

        services.AddHttpClient<IAuthenticationHttpClient, AuthenticationHttpClient>(client => 
        {
            string urlKey = configuration.GetValue<string>("AuthenticationApiSettings:BaseUrl") ?? "";
            string? url = Environment.GetEnvironmentVariable(urlKey) 
                ?? throw new InvalidOperationException("Authentication API base address not found");

            client.BaseAddress = new(url);
        });
    }
}
