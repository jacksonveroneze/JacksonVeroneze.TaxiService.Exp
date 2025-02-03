using JacksonVeroneze.NET.HttpClient.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Payments.Domain.Enums;
using TaxiService.Modules.Payments.Infrastructure.DataProviders.HttpClients;

namespace TaxiService.Modules.Payments.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpClientExtension
{
    public static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddClient<IMercadoPago>(appConfiguration,
            nameof(PaymentProviderType.MercadoPago));

        services.AddClient<IStripe>(appConfiguration,
            nameof(PaymentProviderType.MercadoPago));

        return services;
    }

    private static IServiceCollection AddClient<TClient>(
        this IServiceCollection services,
        AppConfiguration appConfiguration,
        string name) where TClient : class
    {
        HttpClientConfiguration config =
            appConfiguration.HttpClients!.Single(
                client => client.Name!.Equals(name,
                    StringComparison.OrdinalIgnoreCase));

        services.AddHttpClient<TClient>(config);

        return services;
    }
}