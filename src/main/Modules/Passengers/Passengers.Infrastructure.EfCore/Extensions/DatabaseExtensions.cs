using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Modules.Passengers.Application.v1.Interfaces;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Contexts;


namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddDatabase<PassengersDbContext>(
            appConfiguration, appConfiguration.Database!);

        return services;
    }
}