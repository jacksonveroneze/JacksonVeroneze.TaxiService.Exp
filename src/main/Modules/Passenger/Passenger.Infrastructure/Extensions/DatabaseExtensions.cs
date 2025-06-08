using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passenger.Application.Common.Interfaces;
using TaxiService.Modules.Passenger.Infrastructure.Configurations;
using TaxiService.Modules.Passenger.Infrastructure.Contexts;
using TaxiService.Modules.Passenger.Infrastructure.UnitOfWork;

namespace TaxiService.Modules.Passenger.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(
        this IServiceCollection services,
        ModuleConfiguration moduleConfiguration)
    {
        ArgumentNullException.ThrowIfNull(moduleConfiguration);

        services.AddWriteDatabase<PassengersDbContext>(
            moduleConfiguration.Database!);

        services.AddReadDatabase<PassengersReadDbContext>(
            moduleConfiguration.Database!);

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
