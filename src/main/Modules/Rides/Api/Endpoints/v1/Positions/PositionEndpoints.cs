using Asp.Versioning.Builder;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using TaxiService.Modules.Rides.Api.Endpoints.Extensions;

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
                .MapToApiVersion(1)
                .AddFluentValidationAutoValidation();

        builder.AddPosition();

        return app;
    }
}