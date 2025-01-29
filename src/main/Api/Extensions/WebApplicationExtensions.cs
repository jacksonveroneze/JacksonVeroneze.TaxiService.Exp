using CorrelationId;
using Prometheus;
using Scalar.AspNetCore;
using Serilog;
using TaxiService.Modules.Passengers.Api.Endpoints.v1.Users;
using TaxiService.Modules.Rides.Api.Endpoints.v1.Positions;
using TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

namespace TaxiService.Api.Extensions;

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

        app.UseExceptionHandler();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCorrelationId();
        app.UseHttpMetrics();
        app.MapMetrics();

        app.UseHealthChecks("/health");

        app.AddUserEndpoints();
        app.AddRideEndpoints();
        app.AddPositionEndpoints();

        return app;
    }
}