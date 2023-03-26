using Ben.Diagnostics;
using CorrelationId;
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
            .AddMediatr()
            .AddAutoMapper()
            .AddAppServices()
            .AddHealthChecks();

        services
            .AddEndpointsApiExplorer()
            .AddSwagger(appConfiguration)
            .AddAuthentication(appConfiguration)
            .AddAuthorization(appConfiguration)
            .AddOpenTelemetry(appConfiguration)
            .AddCorrelation()
            .AddCultureConfiguration()
            .AddJsonOptionsSerialize();

        return services;
    }

    public static WebApplication Configure(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseBlockingDetection()
                .AddSwagger();
        }

        app.UseCorrelationId()
            .UseRouting()
            .UseHttpMetrics()
            //.UseMiddleware<ErrorHandlingMiddleware>()
            .UseHealthChecks("/health")
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => { endpoints.MapMetrics(); });

        return app;
    }
}


// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
//
// app.UseAuthorization();
//
// app.MapControllers();