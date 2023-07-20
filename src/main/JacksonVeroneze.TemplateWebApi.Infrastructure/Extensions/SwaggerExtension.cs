using JacksonVeroneze.NET.Swagger.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class SwaggerExtension
{
    public static IServiceCollection AddSwagger(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddSwaggerGenerator(conf =>
        {
            conf.Title = appConfiguration.AppName;
            conf.Description = appConfiguration.Application!.Description;
            conf.ContactName = appConfiguration.Swagger!.ContactName;
            conf.ContactEmail = appConfiguration.Swagger.ContactEmail;
            conf.UseAuthentication = true;
            conf.ApiVersion = appConfiguration.AppVersion;
        });

        return services;
    }

    public static IApplicationBuilder AddSwagger(
        this WebApplication app)
    {
        app.UseSwaggerApp();

        return app;
    }
}
