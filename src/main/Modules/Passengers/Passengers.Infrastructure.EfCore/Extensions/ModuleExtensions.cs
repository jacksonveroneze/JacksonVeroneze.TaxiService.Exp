using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPassangerInfraDatabase(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddServices()
            .AddDatabase(appConfiguration);

        return services;
    }
}