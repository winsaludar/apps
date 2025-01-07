namespace Authentication.API.Registrars;

public class DbContextRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("No database connection string found");
        
        services.AddDbContext<AuthenticationDbContext>(options => 
        {
            options.UseNpgsql(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }, ServiceLifetime.Scoped);
    }
}
