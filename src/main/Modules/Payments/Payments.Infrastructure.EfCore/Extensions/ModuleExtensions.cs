using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Modules.Payments.Application.v1;

namespace TaxiService.Modules.Payments.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class ModuleExtensions
{
    public static IServiceCollection AddPaymentInfraDatabase(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddServices()
            .AddDatabase(appConfiguration);

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }
}