using Asp.Versioning.Builder;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Rides;

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
                .MapToApiVersion(1)
                .AddFluentValidationAutoValidation();

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