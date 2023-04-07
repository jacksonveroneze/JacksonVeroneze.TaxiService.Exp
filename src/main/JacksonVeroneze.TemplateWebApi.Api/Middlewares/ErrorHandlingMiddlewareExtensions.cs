namespace JacksonVeroneze.TemplateWebApi.Api.Middlewares;

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomGlobalErrorHandler(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();

        return app;
    }
}