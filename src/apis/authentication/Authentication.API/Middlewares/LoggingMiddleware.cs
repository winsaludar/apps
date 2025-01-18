﻿namespace Authentication.API.Middlewares;

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
            logger.LogInformation("Request Header: {Key}: {Value}", header.Key, header.Value);
        }

        await next(context);

        // Log response details
        logger.LogInformation("Finished handling request. Response Status: {StatusCode}", context.Response.StatusCode);
    }
}