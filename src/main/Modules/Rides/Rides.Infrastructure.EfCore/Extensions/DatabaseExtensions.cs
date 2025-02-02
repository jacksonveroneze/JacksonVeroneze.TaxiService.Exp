using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Rides.Infrastructure.EfCore.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddDatabase<RidesDbContext>(
            appConfiguration, appConfiguration.Database!);

        return services;
    }
}