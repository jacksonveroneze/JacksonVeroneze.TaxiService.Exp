using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Driver.Application.Common.Interfaces;
using TaxiService.Modules.Driver.Infrastructure.Configurations;
using TaxiService.Modules.Driver.Infrastructure.Contexts;
using TaxiService.Modules.Driver.Infrastructure.UnitOfWork;

namespace TaxiService.Modules.Driver.Infrastructure.Extensions;

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
