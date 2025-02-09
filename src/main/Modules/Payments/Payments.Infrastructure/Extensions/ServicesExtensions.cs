using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Factories;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Payment;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Provider;
using TaxiService.Modules.Payments.Infrastructure.Acl.Translators;
using TaxiService.Modules.Payments.Infrastructure.Factories;
using TaxiService.Modules.Payments.Infrastructure.Providers;
using TaxiService.Modules.Payments.Infrastructure.Repositories.Payment;
using TaxiService.Modules.Payments.Infrastructure.Repositories.Provider;

namespace TaxiService.Modules.Payments.Infrastructure.Extensions;

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
        
        #region Payment

        // Repositories
        services.AddScoped<IProviderReadRepository, ProviderReadRepository>();

        #endregion

        services.AddScoped<MercadoPagoTranslator>();
        services.AddScoped<StripeTranslator>();

        services.AddScoped<MercadoPagoProvider>();
        services.AddScoped<StripeProvider>();

        services.AddScoped<IPaymentProviderFactory, PaymentProviderFactory>();
        
        return services;
    }
}