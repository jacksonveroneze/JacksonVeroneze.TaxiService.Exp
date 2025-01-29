using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Domain.Services;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class ServicesExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDistanceCalculatorService, DistanceCalculatorService>();

        return services;
    }
}