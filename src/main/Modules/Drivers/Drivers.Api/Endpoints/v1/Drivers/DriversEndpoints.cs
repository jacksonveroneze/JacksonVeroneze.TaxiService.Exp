using TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers.DriverAvailability;
using TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers.DriverLocation;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

public static class DriversEndpoints
{
    private const string Resource = "drivers";
    private const int Version = 1;
    
    public static WebApplication AddDriverEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder = RouteGroupBuilderFactory
            .Factory(app, Resource, Version);

        builder.AddGetPaged()
            .AddGetById()
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
