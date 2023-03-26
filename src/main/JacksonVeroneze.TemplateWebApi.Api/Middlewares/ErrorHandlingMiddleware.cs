using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace JacksonVeroneze.TemplateWebApi.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public ErrorHandlingMiddleware(RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IHostEnvironment hostEnvironment)
    {
        _next = next;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
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
            Detail = _hostEnvironment.IsDevelopment()
                ? exception.StackTrace + exception.InnerException?.StackTrace
                : string.Empty
        };

        JsonSerializerOptions serializeOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        context.Response.ContentType = "application/problem+json";

        context.Response.StatusCode = problemDetails.Status ??= 500;

        string result = JsonSerializer
            .Serialize(problemDetails, serializeOptions);

        _logger.LogError(result);

        await context.Response.WriteAsync(result);
    }
}