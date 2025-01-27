namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

public static class RideEndpoints
{
    public static WebApplication AddRideEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/rides")
                .WithTags("rides")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1);

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