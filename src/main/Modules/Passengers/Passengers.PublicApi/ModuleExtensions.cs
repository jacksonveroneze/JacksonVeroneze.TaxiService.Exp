using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Passengers.PublicApi.Services;

namespace TaxiService.Modules.Passengers.PublicApi;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPassengerPublicApi(
        this IServiceCollection services)
    {
        services.AddTransient<IPassengerPublicApi, PassengerPublicApi>();

        return services;
    }
}
