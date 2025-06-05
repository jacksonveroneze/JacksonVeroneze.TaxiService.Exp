using CorrelationId;
using Prometheus;
using Scalar.AspNetCore;
using Serilog;
using TaxiService.Modules.Driver.Api.Endpoints.v1.Drivers;
using TaxiService.Modules.Driver.Api.Endpoints.v1.Vehicles;
using TaxiService.Modules.Passenger.Api.Endpoints.v1.Passengers;
using TaxiService.Modules.Ride.Api.Endpoints.v1.Positions;
using TaxiService.Modules.Ride.Api.Endpoints.v1.Rides;

namespace TaxiService.Api.Extensions;

internal static class WebApplicationExtensions
{
    public static WebApplication Configure(
        this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (app.Environment.IsDevelopment())
        {
            app.UseCors("all");
        }

        app.UseRequestLocalization();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseRouting();

        app.UseCorrelationId();
        app.UseHttpMetrics();

        app.UseHealthChecks("/health");
        app.UseMetricServer();
        app.UseAuthentication();
        app.UseAuthorization();

        app.AddDriverEndpoints();
        app.AddVehicleEndpoints();
        app.AddPositionEndpoints();
        app.AddRideEndpoints();
        app.AddPassengerEndpoints();

        app.Lifetime.ApplicationStarted.Register(() =>
            Log.Information(nameof(IHostApplicationLifetime
                .ApplicationStarted)));

        app.Lifetime.ApplicationStopping.Register(() =>
            Log.Information(nameof(IHostApplicationLifetime
                .ApplicationStopping)));

        app.Lifetime.ApplicationStopped.Register(() =>
            Log.Information(nameof(IHostApplicationLifetime
                .ApplicationStopped)));

        return app;
    }
}
