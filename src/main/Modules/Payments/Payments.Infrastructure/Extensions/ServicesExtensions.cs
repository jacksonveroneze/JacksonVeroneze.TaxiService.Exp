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
internal static class ServicesExtensions
{
    internal static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        #region Payment

        // Repositories
        services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
        services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();

        #endregion

        #region Provider

        services.AddScoped<MercadoPagoProvider>();
        services.AddScoped<StripeProvider>();

        // Repositories
        services.AddScoped<IProviderReadRepository, ProviderReadRepository>();

        services.AddScoped<IPaymentProviderFactory, PaymentProviderFactory>();

        #endregion

        #region Translator

        services.AddScoped<MercadoPagoTranslator>();
        services.AddScoped<StripeTranslator>();

        #endregion

        return services;
    }
}
