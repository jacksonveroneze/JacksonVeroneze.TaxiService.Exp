using TaxiService.Modules.Passengers.Api.Endpoints.v1.Passangers.Emails;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passangers;

public static class PassangerEndpoints
{
    public static WebApplication AddPassangerEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/passangers")
                .WithTags("passangers")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1);

        builder.AddGetPaged()
            .AddGetById()
            .AddCreate()
            .AddDelete()
            .AddActivate()
            .AddInactivate()
            .AddEmailCreate()
            .AddEmailDelete()
            .AddGetEmailsByPassangerId();

        return app;
    }
}