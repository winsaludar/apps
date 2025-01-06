using Shared.Common.Behaviors;

namespace Authentication.API.Registrars;

public class MediatorRegistrar : IRegistrar
{
    public void RegistrarService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<RegisterUserCommand>();
        });
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
