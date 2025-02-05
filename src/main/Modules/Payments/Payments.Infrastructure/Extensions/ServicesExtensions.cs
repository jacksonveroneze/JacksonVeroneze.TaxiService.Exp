using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Factories;
using TaxiService.Modules.Payments.Infrastructure.Acl.Translators;
using TaxiService.Modules.Payments.Infrastructure.Factories;
using TaxiService.Modules.Payments.Infrastructure.Providers;

namespace TaxiService.Modules.Payments.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class ServicesExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<MercadoPagoTranslator>();
        services.AddScoped<StripeTranslator>();

        services.AddScoped<MercadoPagoProvider>();
        services.AddScoped<StripeProvider>();

        services.AddScoped<IPaymentProviderFactory, PaymentProviderFactory>();
        
        return services;
    }
}