using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace TaxiService.Common.BaseApi.Endpoints.Extensions;

public static class RouteGroupBuilderFactory
{
    public static RouteGroupBuilder Factory(
        WebApplication app, string resource,
        int version, bool requireAuthorization = true)
    {
        ApiVersionSet apiVersionSet = app.AddVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/" + resource)
                .WithTags(resource)
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(version);

        if (requireAuthorization)
        {
            builder.RequireAuthorization();
        }

        return builder;
    }
}
