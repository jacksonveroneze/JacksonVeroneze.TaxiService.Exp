using JacksonVeroneze.NET.HttpClient.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Payment.Domain.Enums;
using TaxiService.Modules.Payment.Infrastructure.Configurations;
using TaxiService.Modules.Payment.Infrastructure.DataProviders.HttpClients;

namespace TaxiService.Modules.Payment.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class HttpClientExtension
{
    internal static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        ModuleConfiguration moduleConfiguration)
    {
        ArgumentNullException.ThrowIfNull(moduleConfiguration);

        services.AddClient<IMercadoPago>(moduleConfiguration,
            nameof(PaymentProviderType.MercadoPago));

        services.AddClient<IStripe>(moduleConfiguration,
            nameof(PaymentProviderType.Stripe));

        return services;
    }

    private static IServiceCollection AddClient<TClient>(
        this IServiceCollection services,
        ModuleConfiguration moduleConfiguration,
        string name) where TClient : class
    {
        HttpClientConfiguration config =
            moduleConfiguration.HttpClients!.Single(client =>
                client.Name!.Equals(name,
                    StringComparison.OrdinalIgnoreCase));

        services.AddHttpClient<TClient>(config);

        return services;
    }
}
