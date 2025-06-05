using TaxiService.Modules.Passenger.Api.Endpoints.v1.Passengers.Emails;

namespace TaxiService.Modules.Passenger.Api.Endpoints.v1.Passengers;

public static class PassengersEndpoints
{
    private const string Resource = "passengers";
    private const int Version = 1;

    public static WebApplication AddPassengerEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder = RouteGroupBuilderFactory
            .Factory(app, Resource, Version);

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
