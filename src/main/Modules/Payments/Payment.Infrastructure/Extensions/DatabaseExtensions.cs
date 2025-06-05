using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Payment.Application.Common.Interfaces;
using TaxiService.Modules.Payment.Infrastructure.Configurations;
using TaxiService.Modules.Payment.Infrastructure.Contexts;
using TaxiService.Modules.Payment.Infrastructure.UnitOfWork;

namespace TaxiService.Modules.Payment.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(
        this IServiceCollection services,
        ModuleConfiguration moduleConfiguration)
    {
        ArgumentNullException.ThrowIfNull(moduleConfiguration);

        services.AddWriteDatabase<PaymentsDbContext>(
            moduleConfiguration.Database!);

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
