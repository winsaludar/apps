namespace Authentication.API.Registrars;

public class DbContextRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__db")
            ?? throw new InvalidOperationException("Database connection string not found");

        services.AddDbContext<AuthenticationDbContext>(options => 
        {
            options.UseNpgsql(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }, ServiceLifetime.Scoped);
    }
}
