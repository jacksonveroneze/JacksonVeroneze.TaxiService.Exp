using Microsoft.Extensions.DependencyInjection;

using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passengers.Application.Common.Interfaces;
using TaxiService.Modules.Passengers.Infrastructure.Configurations;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(
        this IServiceCollection services,
        ModuleConfiguration moduleConfiguration)
    {
        ArgumentNullException.ThrowIfNull(moduleConfiguration);

        services.AddDatabase<PassengersDbContext>(
            moduleConfiguration.Database!);
        
        services.AddReadDatabase<PassengersReadDbContext>(
            moduleConfiguration.Database!);
        
        services.AddScoped<IUnitOfWork, UnitOfWork.EfUnitOfWork>();
        
        return services;
    }
}
