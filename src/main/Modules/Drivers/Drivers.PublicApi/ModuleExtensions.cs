using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Drivers.PublicApi.Interfaces;
using TaxiService.Modules.Drivers.PublicApi.Services;

namespace TaxiService.Modules.Drivers.PublicApi;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddDriverPublicApi(
        this IServiceCollection services)
    {
        services.AddTransient<IDriverPublicApi, DriverPublicApi>();

        return services;
    }
}