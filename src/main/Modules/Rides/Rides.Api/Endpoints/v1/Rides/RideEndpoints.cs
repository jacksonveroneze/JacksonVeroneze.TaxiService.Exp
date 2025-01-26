using Asp.Versioning.Builder;
using TaxiService.Common.BaseApi.Endpoints.Extensions;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class RideEndpoints
{
    public static WebApplication AddRideEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/rides")
                .RequireAuthorization()
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