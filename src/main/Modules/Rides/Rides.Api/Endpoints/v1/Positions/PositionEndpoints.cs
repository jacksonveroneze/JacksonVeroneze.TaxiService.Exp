namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Positions;

public static class PositionEndpoints
{
    public static WebApplication AddPositionEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/positions")
                .WithTags("positions")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1);

        builder.AddPosition();

        return app;
    }
}