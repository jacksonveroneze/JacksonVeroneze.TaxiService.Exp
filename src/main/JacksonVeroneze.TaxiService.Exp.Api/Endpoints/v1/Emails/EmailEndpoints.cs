using Asp.Versioning.Builder;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Emails;

internal static class EmailEndpoints
{
    public static WebApplication AddEmailEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/emails")
                .WithTags("emails")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1);

        builder.AddCreate()
            .AddDelete()
            .AddGetByUserId();

        return app;
    }
}