namespace TaxiService.Modules.Ride.Api.Endpoints.v1.Positions;

public static class PositionsEndpoints
{
    private const string Resource = "positions";
    private const int Version = 1;

    public static WebApplication AddPositionEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder = RouteGroupBuilderFactory
            .Factory(app, Resource, Version);

        builder.AddPosition();

        return app;
    }
}
