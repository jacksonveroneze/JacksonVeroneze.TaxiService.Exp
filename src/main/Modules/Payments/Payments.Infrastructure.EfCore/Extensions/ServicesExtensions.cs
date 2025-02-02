using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Payment;
using TaxiService.Modules.Payments.Infrastructure.EfCore.Repositories.Payment;

namespace TaxiService.Modules.Payments.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        #region Payment

        // Repositories
        services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
        services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();

        #endregion

        return services;
    }
}