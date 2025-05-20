using TaxiService.Modules.Passengers.Api.Endpoints.v1.Passenger.Emails;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passenger;

public static class PassengerEndpoints
{
    public static WebApplication AddPassengerEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/passengers")
                .WithTags("passengers")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1);

        builder.AddGetPaged()
            .AddGetById()
            .AddCreate()
            .AddDelete()
            .AddActivate()
            .AddInactivate()
            .AddEmailCreate()
            .AddEmailDelete()
            .AddGetEmailsByPassengerId();

        return app;
    }
}
