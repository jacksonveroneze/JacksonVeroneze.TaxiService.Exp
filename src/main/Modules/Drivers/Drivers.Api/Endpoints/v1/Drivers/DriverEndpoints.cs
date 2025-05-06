using TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers.DriverAvailability;
using TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers.DriverLocation;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

public static class DriverEndpoints
{
    public static WebApplication AddDriverEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/drivers")
                .WithTags("drivers")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1);

        builder.AddGetById()
            .AddGetPaged()
            .AddCreate()
            .AddActivate()
            .AddInactivate()
            .AddReactivate()
            .AddBlock()
            .AddDelete()
            .UpdateAvailability()
            .UpdateLocationCommand();

        return app;
    }
}
