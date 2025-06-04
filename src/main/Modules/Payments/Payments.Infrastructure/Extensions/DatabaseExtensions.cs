using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Payments.Application.v1;
using TaxiService.Modules.Payments.Infrastructure.Configurations;
using TaxiService.Modules.Payments.Infrastructure.Contexts;

namespace TaxiService.Modules.Payments.Infrastructure.Extensions;

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

        services.AddScoped<IUnitOfWork, UnitOfWork.EfUnitOfWork>();

        return services;
    }
}
