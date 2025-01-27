using Microsoft.Extensions.DependencyInjection;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class AppServicesExtensions
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services)
    {
        return services;
    }
}