using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Factories;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Payment.Domain.Repositories;
using TaxiService.Modules.Payment.Infrastructure.Acl.Translators;
using TaxiService.Modules.Payment.Infrastructure.Factories;
using TaxiService.Modules.Payment.Infrastructure.Providers;
using TaxiService.Modules.Payment.Infrastructure.Repositories.Payment;
using TaxiService.Modules.Payment.Infrastructure.Repositories.Provider;

namespace TaxiService.Modules.Payment.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class ServicesExtensions
{
    internal static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        #region Payment

        // Repositories
        services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

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
