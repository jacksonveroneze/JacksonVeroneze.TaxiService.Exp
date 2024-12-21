using CorrelationId;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Emails;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Positions;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Rides;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Users;
using Prometheus;
using Scalar.AspNetCore;
using Serilog;

namespace JacksonVeroneze.TaxiService.Exp.Api.Extensions;

internal static class WebApplicationExtensions
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
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCorrelationId();
        app.UseHttpMetrics();
        app.MapMetrics();
        app.UseExceptionHandler();

        app.UseHealthChecks("/health");

        app.MapGraphQL();

        app.AddUserEndpoints();
        app.AddEmailEndpoints();
        app.AddRideEndpoints();
        app.AddPositionEndpoints();

        return app;
    }
}