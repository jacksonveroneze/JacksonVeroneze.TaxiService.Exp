using Asp.Versioning.Builder;
using TaxiService.Common.BaseApi.Endpoints.Extensions;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Positions;

internal static class PositionEndpoints
{
    public static WebApplication AddPositionEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/positions")
                .RequireAuthorization()
                .WithTags("positions")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1);

        builder.AddPosition();

        return app;
    }
}