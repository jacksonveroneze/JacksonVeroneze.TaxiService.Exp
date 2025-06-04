using Microsoft.Extensions.Logging;
using Refit;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Providers;
using TaxiService.Modules.Payments.Domain.Entities;
using TaxiService.Modules.Payments.Domain.Enums;
using TaxiService.Modules.Payments.Domain.Models;
using TaxiService.Modules.Payments.Infrastructure.Acl.Translators;
using TaxiService.Modules.Payments.Infrastructure.DataProviders.HttpClients;
using TaxiService.Modules.Payments.Infrastructure.Extensions;

namespace TaxiService.Modules.Payments.Infrastructure.Providers;

internal sealed class StripeProvider(
    ILogger<StripeProvider> logger,
    IStripe client,
    StripeTranslator translator) : IPaymentProvider
{
    public PaymentProviderType Identifier
        => PaymentProviderType.MercadoPago;

    public async Task<PaymentResult> PayAsync(
        PaymentEntity payment,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(payment);

        try
        {
            var request = translator.TranslateToProvider(payment);

            var result = await client.SignAsync
                (request, cancellationToken);

            return translator.TranslateFromProvider(result);
        }
        catch (ApiException ex)
        {
            logger.LogGenericHttpError(nameof(StripeProvider),
                nameof(PayAsync), ex.StatusCode, ex);

            return PaymentResult.WithError(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogGenericError(nameof(StripeProvider),
                nameof(PayAsync), ex);

            return PaymentResult.WithError(ex.Message);
        }
    }
}
