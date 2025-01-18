namespace Budget.API.Middlewares;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        (httpContext.Response.StatusCode, string message) = exception switch
        {
            ExpenseException => (StatusCodes.Status400BadRequest, "Invalid input"),
            BadRequestException => (StatusCodes.Status400BadRequest, "Invalid input"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access"),
            NotFoundException => (StatusCodes.Status404NotFound, "Not found"),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error")
        };

        ErrorResponse response = new(httpContext.Response.StatusCode, message, exception.Message.Split("|"));

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, options: new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }
}
