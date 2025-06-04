namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Vehicles;

public static class VehiclesEndpoints
{
    private const string Resource = "vehicles";
    private const int Version = 1;

    public static WebApplication AddVehicleEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder = RouteGroupBuilderFactory
            .Factory(app, Resource, Version);

        builder.AddGetById()
            .AddCreate();

        return app;
    }
}
