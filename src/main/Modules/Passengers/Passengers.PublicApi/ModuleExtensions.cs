using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Passengers.PublicApi.Services;

namespace TaxiService.Modules.Passengers.PublicApi;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPassangerPublicApi(
        this IServiceCollection services)
    {
        services.AddTransient<IPassangerService, PassangerService>();

        return services;
    }
}