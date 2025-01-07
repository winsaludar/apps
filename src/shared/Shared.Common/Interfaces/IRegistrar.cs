namespace Shared.Common.Interfaces;

public interface IRegistrar
{
    void RegistrarService(IServiceCollection services, IConfiguration configuration);
}
