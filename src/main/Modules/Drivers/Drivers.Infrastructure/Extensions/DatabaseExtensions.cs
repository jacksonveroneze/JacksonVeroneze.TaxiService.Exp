using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Drivers.Application.Common.Interfaces;
using TaxiService.Modules.Drivers.Infrastructure.Configurations;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;
using TaxiService.Modules.Drivers.Infrastructure.UnitOfWork;

namespace TaxiService.Modules.Drivers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(
        this IServiceCollection services,
        ModuleConfiguration moduleConfiguration)
    {
        ArgumentNullException.ThrowIfNull(moduleConfiguration);

        services.AddWriteDatabase<DriversDbContext>(
            moduleConfiguration.Database!);

        services.AddReadDatabase<DriversReadDbContext>(
            moduleConfiguration.Database!);

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
