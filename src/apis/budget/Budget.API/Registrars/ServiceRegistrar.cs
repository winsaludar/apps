namespace Budget.API.Registrars;

public class ServiceRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
    }
}
