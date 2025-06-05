using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Factories;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Providers;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Payment.Domain;
using TaxiService.Modules.Payment.Domain.Aggregates;
using TaxiService.Modules.Payment.Domain.Enums;
using TaxiService.Modules.Payment.Infrastructure.Extensions;
using TaxiService.Modules.Payment.Infrastructure.Providers;

namespace TaxiService.Modules.Payment.Infrastructure.Factories;

internal sealed class PaymentProviderFactory(
    ILogger<PaymentProviderFactory> logger,
    IServiceProvider serviceProvider,
    IProviderReadRepository providerRepository)
    : IPaymentProviderFactory
{
    public async Task<ImmutableArray<Lazy<IPaymentProvider>>> GetProvidersAsync(
        CancellationToken cancellationToken)
    {
        ImmutableArray<Provider> providersConfigs =
            await LoadProviderConfigsAsync(cancellationToken);

        ImmutableArray<Lazy<IPaymentProvider>> providers = 
            CreateProviders(providersConfigs);

        return providers;
    }

    private async Task<ImmutableArray<Provider>> LoadProviderConfigsAsync(
        CancellationToken cancellationToken)
    {
        logger.LogLoadProviders(ModuleConstants.ContextName);

        ImmutableArray<Provider> configs = await providerRepository
            .GetAllAsync(cancellationToken);

        if (!configs.Any())
        {
            logger.LogNoProvidersFound(ModuleConstants.ContextName);

            return [];
        }

        IEnumerable<PaymentProviderType> nameProviders = 
            configs.Select(prov => prov.ProviderName);

        logger.LogProvidersFounded(
            ModuleConstants.ContextName,
            configs.Length, nameProviders.ToArray());

        return configs;
    }

    private ImmutableArray<Lazy<IPaymentProvider>> CreateProviders(
        ImmutableArray<Provider> providerConfigs)
    {
        ImmutableArray<Lazy<IPaymentProvider>> providers = providerConfigs
            .Select(config => CreateProvider(config.ProviderName))
            .ToImmutableArray();

        return providers;
    }

    private Lazy<IPaymentProvider> CreateProvider(
        PaymentProviderType providerType)
    {
        logger.LogStartCreateProvider(ModuleConstants.ContextName, providerType);

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
