using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthenticationExtension
{
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        return services;
    }
}