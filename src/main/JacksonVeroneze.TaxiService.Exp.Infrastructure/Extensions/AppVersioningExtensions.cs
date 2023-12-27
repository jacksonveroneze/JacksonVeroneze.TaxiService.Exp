using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppVersioningExtensions
{
    public static IServiceCollection AddAppVersioning(
        this IServiceCollection services)
    {
        return services;
    }
}