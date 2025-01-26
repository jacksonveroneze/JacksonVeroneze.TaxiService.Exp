using Microsoft.AspNetCore.Builder;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class SwaggerExtensions
{
    public static IApplicationBuilder AddSwagger(
        this IApplicationBuilder app)
    {
        return app;
    }
}