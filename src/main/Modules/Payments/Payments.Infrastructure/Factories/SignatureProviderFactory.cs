using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Factories;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Providers;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Provider;
using TaxiService.Modules.Payments.Domain.Entities;
using TaxiService.Modules.Payments.Domain.Enums;
using TaxiService.Modules.Payments.Infrastructure.Extensions;
using TaxiService.Modules.Payments.Infrastructure.Providers;

namespace TaxiService.Modules.Payments.Infrastructure.Factories;

internal sealed class PaymentProviderFactory(
    ILogger<PaymentProviderFactory> logger,
    IServiceProvider serviceProvider,
    IProviderReadRepository providerRepository)
    : IPaymentProviderFactory
{
    public async Task<IReadOnlyCollection<Lazy<IPaymentProvider>>> GetProvidersAsync(
        CancellationToken cancellationToken)
    {
        var providersConfigs =
            await LoadProviderConfigsAsync(cancellationToken);

        var providers = CreateProviders(providersConfigs);

        return providers;
    }

    private async Task<IReadOnlyCollection<ProviderEntity>> LoadProviderConfigsAsync(
        CancellationToken cancellationToken)
    {
        logger.LogLoadProviders();

        var configs = await providerRepository
            .GetAllAsync(cancellationToken);

        if (configs.Count == 0)
        {
            logger.LogNoProvidersFound();

            return Array.Empty<ProviderEntity>();
        }

        var nameProviders = configs.Select(
            prov => prov.ProviderName);

        logger.LogProvidersFounded(
            configs.Count, nameProviders.ToArray());

        return configs;
    }

    private IReadOnlyCollection<Lazy<IPaymentProvider>> CreateProviders(
        IReadOnlyCollection<ProviderEntity> providerConfigs)
    {
        var providers = providerConfigs
            .Select(config => CreateProvider(config.ProviderName))
            .ToImmutableArray();

        return providers;
    }

    private Lazy<IPaymentProvider> CreateProvider(
        PaymentProviderType providerType)
    {
        logger.LogStartCreateProvider(providerType);

        return providerType switch
        {
            PaymentProviderType.MercadoPago =>
                CreateProviderInternal<MercadoPagoProvider>(),

            PaymentProviderType.Stripe =>
                CreateProviderInternal<StripeProvider>(),

            _ => throw new ArgumentException(
                $"Unknown provider type '{providerType}'",
                nameof(providerType))
        };
    }

    private Lazy<IPaymentProvider> CreateProviderInternal<TType>()
        where TType : class, IPaymentProvider
    {
        return new Lazy<IPaymentProvider>(serviceProvider
            .GetRequiredService<TType>);
    }
}