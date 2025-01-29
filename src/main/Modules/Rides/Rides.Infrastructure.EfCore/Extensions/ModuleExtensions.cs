using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddRideInfraDatabase(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddServices()
            .AddDatabase(appConfiguration);

        return services;
    }
}