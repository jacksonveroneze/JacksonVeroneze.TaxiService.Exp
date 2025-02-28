using TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers.DriverAvailability;

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

        builder.AddCreate()
            .AddActivate()
            .AddInactivate()
            .AddReactivate()
            .AddBlock()
            .AddDelete()
            .AddGetById()
            .AddUpdateAvailability()
            .AddUpdateLocationCommand();

        return app;
    }
}