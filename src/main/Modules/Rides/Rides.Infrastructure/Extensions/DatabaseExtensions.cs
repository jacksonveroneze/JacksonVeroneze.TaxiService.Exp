using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Rides.Application.Common.Interfaces;
using TaxiService.Modules.Rides.Infrastructure.Configurations;
using TaxiService.Modules.Rides.Infrastructure.Contexts;
using TaxiService.Modules.Rides.Infrastructure.UnitOfWork;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

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
