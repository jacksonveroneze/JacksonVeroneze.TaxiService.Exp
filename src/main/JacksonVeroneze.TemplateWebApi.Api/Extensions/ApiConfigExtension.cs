using CorrelationId;
using JacksonVeroneze.TemplateWebApi.Api.Middlewares;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;
using Prometheus;

namespace JacksonVeroneze.TemplateWebApi.Api.Extensions;

public static class ApiConfigExtension
{
    public static WebApplicationBuilder ConfigureServices(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        builder.Services
            .AddControllers()
            .AddJsonOptionsSerialize()
            .ConfigureApiBehaviorOptions(options =>
                options.SuppressInferBindingSourcesForParameters = true);

        if (!builder.Environment.IsProduction())
        {
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwagger(appConfiguration);
        }

        builder.Services
            .AddAppServices()
            .AddAutoMapper()
            .AddCorrelation()
            .AddMediatr()
            .AddFluentValidation()
            .AddAuthentication(appConfiguration)
            .AddAuthorization(appConfiguration)
            .AddOpenTelemetry(appConfiguration)
            .AddCached(appConfiguration)
            .AddDistributedLock(appConfiguration)
            .AddHttpClients(appConfiguration)
            .AddCultureConfiguration()
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddHealthChecks();

        return builder;
    }

    public static WebApplication Configure(
        this WebApplication app)
    {
        // if (app.Environment.IsDevelopment())
        // {
        //     //app.UseBlockingDetection();
        //     //app.UseMiniProfiler();
        // }

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
