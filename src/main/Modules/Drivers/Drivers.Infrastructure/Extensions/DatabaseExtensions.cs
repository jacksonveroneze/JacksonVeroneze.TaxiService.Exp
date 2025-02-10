using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Drivers.Application.v1.Interfaces;
using TaxiService.Modules.Drivers.Infrastructure.Configurations;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(
        this IServiceCollection services,
        ModuleConfiguration moduleConfiguration)
    {
        ArgumentNullException.ThrowIfNull(moduleConfiguration);

        services.AddDatabase<DriversDbContext>(
            moduleConfiguration.Database!);

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        
        return services;
    }
}