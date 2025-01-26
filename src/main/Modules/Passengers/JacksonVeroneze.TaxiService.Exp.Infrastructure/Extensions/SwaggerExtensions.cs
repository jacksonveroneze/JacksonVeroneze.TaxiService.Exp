using Microsoft.AspNetCore.Builder;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class SwaggerExtensions
{
    public static IApplicationBuilder AddSwagger(
        this IApplicationBuilder app)
    {
        return app;
    }
}