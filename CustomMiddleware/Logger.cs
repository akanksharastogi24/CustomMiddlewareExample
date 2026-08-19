using System.Diagnostics;

namespace CustomMiddlewareExample.CustomMiddleware;

public class CustomLoggerMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomLoggerMiddleWare> _logger;

    public CustomLoggerMiddleWare(RequestDelegate next, ILogger<CustomLoggerMiddleWare> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;

        _logger.LogInformation(
            "Incoming HTTP {Method} {Path}{QueryString}",
            request.Method,
            request.Path,
            request.QueryString);

        try
        {
            // Call the next middleware in the pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception occurred while processing HTTP {Method} {Path}",
                request.Method,
                request.Path);
            throw;
        }
        finally
        {
            stopwatch.Stop();

            _logger.LogInformation(
                "Completed HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
                request.Method,
                request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
