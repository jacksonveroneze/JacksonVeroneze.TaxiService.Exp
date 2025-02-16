namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Vehicle;

public static class VehicleEndpoints
{
    public static WebApplication AddVehicleEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/vehicles")
                .WithTags("vehicles")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1);

        builder.AddCreate();

        return app;
    }
}