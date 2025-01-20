namespace Budget.API.Middlewares;

public class LoggingMiddleware(ILogger<LoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Log request details
        logger.LogInformation("Handling request: {Method} {Path} with QueryString: {QueryString}",
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString);

        foreach (var header in context.Request.Headers)
        {
            string encodedValue = HttpUtility.HtmlEncode(header.Value);
            logger.LogInformation("Request Header: {Key}: {Value}", header.Key, encodedValue);
        }

        await next(context);

        // Log response details
        logger.LogInformation("Finished handling request. Response Status: {StatusCode}", context.Response.StatusCode);
    }
}
