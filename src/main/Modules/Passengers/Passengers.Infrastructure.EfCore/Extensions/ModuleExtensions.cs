using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Modules.Passengers.Infrastructure.Extensions;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPassangerInfraDatabase(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddAppServices()
            .AddDatabase(appConfiguration);

        return services;
    }
}