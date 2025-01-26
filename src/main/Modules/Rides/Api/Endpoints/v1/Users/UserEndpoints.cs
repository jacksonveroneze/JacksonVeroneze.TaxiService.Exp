using Asp.Versioning.Builder;
using TaxiService.Modules.Rides.Api.Endpoints.Extensions;
using TaxiService.Modules.Rides.Api.Endpoints.v1.Users.Emails;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Users;

internal static class UserEndpoints
{
    public static WebApplication AddUserEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/users")
                .WithTags("users")
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
            .AddGetEmailsByUserId();

        return app;
    }
}