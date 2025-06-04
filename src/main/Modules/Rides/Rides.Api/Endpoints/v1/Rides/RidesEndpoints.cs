namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

public static class RidesEndpoints
{
    private const string Resource = "rides";
    private const int Version = 1;

    public static WebApplication AddRideEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder = RouteGroupBuilderFactory
            .Factory(app, Resource, Version);

        builder.AddGetPaged()
            .AddGetById()
            .AddRequest()
            .AddAccept()
            .AddStart()
            .AddFinish()
            .AddCancel();

        return app;
    }
}
