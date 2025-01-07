namespace Authentication.API.Registrars;

public class MediatorRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<RegisterUserCommand>();
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }
}
