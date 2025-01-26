using Microsoft.AspNetCore.Builder;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class SwaggerExtensions
{
    public static IApplicationBuilder AddSwagger(
        this IApplicationBuilder app)
    {
        return app;
    }
}