namespace BlazorApp.Registrars;

public static class RegistrarBuilderExtensions
{
    public static void AddRegistrarServices(this IServiceCollection services, IConfiguration configuration)
    {
        var installers = typeof(Program).Assembly.ExportedTypes
            .Where(x => typeof(IRegistrar).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IRegistrar>().ToList();

        installers.ForEach(installer => installer.RegistrarService(services, configuration));
    }
}