namespace Budget.API.Registrars;

public class ServiceRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
    }
}
