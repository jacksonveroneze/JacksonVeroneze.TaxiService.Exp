using Ben.Diagnostics;
using CorrelationId;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints;
using JacksonVeroneze.TaxiService.Exp.Api.Middlewares;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;
using Prometheus;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseExceptionHandler();

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

        app.UseHttpMetrics()
            .UseCorrelationId();

        app.MapMetrics();
        app.UseHealthChecks("/health");

        app.AddUserEndpoints();
        app.AddRideEndpoints();
        app.AddPositionEndpoints();

        return app;
    }
}