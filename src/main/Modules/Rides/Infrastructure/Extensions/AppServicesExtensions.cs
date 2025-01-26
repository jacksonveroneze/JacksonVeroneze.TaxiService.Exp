using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Domain.Services;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDistanceCalculatorService, DistanceCalculatorService>();

        return services;
    }
}