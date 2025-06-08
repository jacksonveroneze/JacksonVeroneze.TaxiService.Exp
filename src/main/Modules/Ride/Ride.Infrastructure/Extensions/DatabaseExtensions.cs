using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Ride.Application.Common.Interfaces;
using TaxiService.Modules.Ride.Infrastructure.Configurations;
using TaxiService.Modules.Ride.Infrastructure.Contexts;
using TaxiService.Modules.Ride.Infrastructure.UnitOfWork;

namespace TaxiService.Modules.Ride.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(
        this IServiceCollection services,
        ModuleConfiguration moduleConfiguration)
    {
        ArgumentNullException.ThrowIfNull(moduleConfiguration);

        services.AddWriteDatabase<RidesDbContext>(
            moduleConfiguration.Database!);

        services.AddReadDatabase<RidesReadDbContext>(
            moduleConfiguration.Database!);

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
