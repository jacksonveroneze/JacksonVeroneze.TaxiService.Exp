using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Middlewares;

public class ErrorHandlingMiddleware(
    RequestDelegate next,
    ILogger<ErrorHandlingMiddleware> logger,
    IHostEnvironment hostEnvironment)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);

            await FactoryResponse(context, e,
                HttpStatusCode.InternalServerError);
        }
    }

    private async Task FactoryResponse(
        HttpContext context,
        Exception exception,
        HttpStatusCode statusCode)
    {
        ProblemDetails problemDetails = new()
        {
            Instance = context.Request.Path,
            Title = exception.Message,
            Status = (int)statusCode,
            Detail = hostEnvironment.IsDevelopment()
                ? exception.StackTrace + exception.InnerException?.StackTrace
                : string.Empty
        };

        JsonSerializerOptions serializeOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true
        };

        context.Response.ContentType = "application/problem+json";

        context.Response.StatusCode = problemDetails.Status ??= 500;

        string result = JsonSerializer
            .Serialize(problemDetails, serializeOptions);

        await context.Response.WriteAsync(result,
            cancellationToken: context.RequestAborted);
    }
}
