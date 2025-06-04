using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;

namespace TaxiService.Common.BaseApi.Endpoints.Extensions;

public static class ApiVersionExtensions
{
    public static ApiVersionSet AddVersion(
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
