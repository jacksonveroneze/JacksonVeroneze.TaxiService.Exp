using Asp.Versioning.Builder;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Positions;

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