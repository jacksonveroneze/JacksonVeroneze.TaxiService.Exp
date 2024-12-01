using Asp.Versioning;
using Asp.Versioning.Builder;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;

internal static class ApiVersionExtensions
{
    public static ApiVersionSet ApiVersion(
        this WebApplication app,
        params int[]? versions)
    {
        ApiVersionSetBuilder apiVersionSetBuilder =
            app.NewApiVersionSet()
                .ReportApiVersions();

        versions ??= [1];

        foreach (int version in versions)
        {
            apiVersionSetBuilder.HasApiVersion(
                new ApiVersion(version));
        }

        return apiVersionSetBuilder.Build();
    }
}