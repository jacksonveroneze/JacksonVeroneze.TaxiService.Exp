using JacksonVeroneze.NET.Swagger.Extensions;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class SwaggerExtension
{
    public static IApplicationBuilder AddSwagger(
        this IApplicationBuilder app)
    {
        app.UseSwagger()
            .UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                conf.RoutePrefix = string.Empty;
            });

        return app;
    }
}
