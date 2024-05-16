using Ben.Diagnostics;
using CorrelationId;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;
using Prometheus;
using Serilog;

namespace JacksonVeroneze.TaxiService.Exp.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.Lifetime.ApplicationStarted.Register(() =>
            Log.Information("ApplicationStarted"));

        app.Lifetime.ApplicationStopping.Register(() =>
            Log.Information("ApplicationStopping"));

        app.Lifetime.ApplicationStopped.Register(() =>
            Log.Information("ApplicationStopped"));

        if (app.Environment.IsDevelopment())
        {
            app.UseBlockingDetection()
                .AddSwagger();
        }
        app.UseCorrelationId();
        app.UseRouting();
        app.UseHttpMetrics();
        app.MapMetrics();
        app.UseExceptionHandler();
        app.UseOpenTelemetryPrometheusScrapingEndpoint("/metrics-open");

        app.UseHealthChecks("/health");

        app.AddUserEndpoints();
        app.AddRideEndpoints();
        app.AddPositionEndpoints();

        return app;
    }
}