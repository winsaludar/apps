namespace Authentication.API.Registrars;

public class MiddlewareRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
    }
}