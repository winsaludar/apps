namespace Budget.API.Registrars;

public static class EndpointBuilderExtensions
{
    public static void MapCustomEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = typeof(Program).Assembly.GetTypes()
            .Where(x => typeof(IEndpoint).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IEndpoint>().ToList();

        endpoints.ForEach(e => e.MapEndpoint(app));
    }
}
