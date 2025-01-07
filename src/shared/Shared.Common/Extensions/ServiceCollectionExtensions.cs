namespace Shared.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSettings<T>(this IServiceCollection services, string key, IConfiguration configuration) where T : class, new()
    {
        T settings = new();
        configuration.GetSection(key).Bind(settings);
        services.AddSingleton(settings);
    }
}
