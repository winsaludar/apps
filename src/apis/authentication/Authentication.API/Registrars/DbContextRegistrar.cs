namespace Authentication.API.Registrars;

public class DbContextRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        string connectionStringKey = configuration.GetConnectionString("DefaultConnection") ?? "";
        string connectionString = Environment.GetEnvironmentVariable(connectionStringKey)
            ?? throw new InvalidOperationException("Database connection string not found");

        services.AddDbContext<AuthenticationDbContext>(options => 
        {
            options.UseNpgsql(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }, ServiceLifetime.Scoped);
    }
}
