using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TaxiService.Common.BaseApi.Middlewares;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IProblemDetailsService problemDetailsService)
    : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);

        const int defaultStatusCode =
            (int)HttpStatusCode.InternalServerError;

        ProblemDetailsContext problemDetailsContext = new()
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = "An error occurred",
                Detail = exception.Message,
                Type = exception.GetType().Name,
                Status = defaultStatusCode,
            },
        };

        httpContext.Response.StatusCode =
            (int)HttpStatusCode.InternalServerError;

        return problemDetailsService
            .TryWriteAsync(problemDetailsContext);
    }
}
