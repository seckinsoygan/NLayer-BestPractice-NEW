﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Services.ExceptionHandlers;
public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var errorAsDto = ServiceResult.Fail(exception.Message, System.Net.HttpStatusCode.InternalServerError);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken);
        return true;
    }
}
