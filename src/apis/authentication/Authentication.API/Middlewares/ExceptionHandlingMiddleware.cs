namespace Authentication.API.Middlewares;

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
            BadRequestException => (StatusCodes.Status400BadRequest, "Invalid input"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized Access"),
            NotFoundException => (StatusCodes.Status404NotFound, "Not found"),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error")
        };

        var response = new 
        { 
            statusCode = httpContext.Response.StatusCode,
            error = message,
            details = exception.Message.Split("|") 
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

