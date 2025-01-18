namespace Budget.API.Registrars;

public class MediatorRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        //services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<GetExpensesQuery>();
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }
}
