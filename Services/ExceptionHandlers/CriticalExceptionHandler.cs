using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Services.ExceptionHandlers;
public class CriticalExceptionHandler(ILogger<CriticalExceptionHandler> logger) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is CriticalException)
        {
            logger.LogCritical(exception, "A critical exception occurred: {Message}", exception.Message);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";
            _ = System.Text.Json.JsonSerializer.Serialize(new { error = "A critical error occurred. Please contact support." });
        }
        return ValueTask.FromResult(false);
    }
}
