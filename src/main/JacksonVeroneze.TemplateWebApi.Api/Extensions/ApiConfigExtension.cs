using Ben.Diagnostics;
using CorrelationId;
using JacksonVeroneze.TemplateWebApi.Api.Middlewares;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;
using Prometheus;

namespace JacksonVeroneze.TemplateWebApi.Api.Extensions;

public static class ApiConfigExtension
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services
            .AddControllers()
            .AddJsonOptionsSerialize()
            .ConfigureApiBehaviorOptions(options =>
                options.SuppressInferBindingSourcesForParameters = true);

        services
            .AddEndpointsApiExplorer()
            .AddSwagger(appConfiguration)
            .AddAppServices()
            .AddAutoMapper()
            .AddCorrelation()
            .AddMediatr()
            .AddFluentValidation()
            .AddAppVersioning()
            .AddAuthentication(appConfiguration)
            .AddAuthorization(appConfiguration)
            .AddOpenTelemetry(appConfiguration)
            .AddCached(appConfiguration)
            .AddHttpClients(appConfiguration)
            .AddCultureConfiguration()
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddHealthChecks();

        return services;
    }

    public static WebApplication Configure(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseBlockingDetection();
            app.AddSwagger();
        }

        app.UseHttpMetrics()
            .UseCorrelationId()
            .UseCustomGlobalErrorHandler()
            .UseAuthentication()
            .UseAuthorization();

        app.MapMetrics();
        app.UseHealthChecks("/health");

        app.MapControllers();

        return app;
    }
}