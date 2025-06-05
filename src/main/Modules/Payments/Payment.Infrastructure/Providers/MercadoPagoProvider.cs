using Microsoft.Extensions.Logging;
using Refit;
using TaxiService.Modules.Payment.Application.Common.Interfaces.Providers;
using TaxiService.Modules.Payment.Domain.Enums;
using TaxiService.Modules.Payment.Domain.Models;
using TaxiService.Modules.Payment.Infrastructure.Acl.Models.MercadoPago;
using TaxiService.Modules.Payment.Infrastructure.Acl.Translators;
using TaxiService.Modules.Payment.Infrastructure.DataProviders.HttpClients;
using TaxiService.Modules.Payment.Infrastructure.Extensions;

namespace TaxiService.Modules.Payment.Infrastructure.Providers;

internal sealed class MercadoPagoProvider(
    ILogger<MercadoPagoProvider> logger,
    IMercadoPago client,
    MercadoPagoTranslator translator) : IPaymentProvider
{
    public PaymentProviderType Identifier
        => PaymentProviderType.MercadoPago;

    public async Task<PaymentResult> PayAsync(
        Domain.Aggregates.Payment payment,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(payment);

        try
        {
            PayRequest request = translator.TranslateToProvider(payment);

            PayResponse result = await client.SignAsync
                (request, cancellationToken);

            return translator.TranslateFromProvider(result);
        }
        catch (ApiException ex)
        {
            logger.LogGenericHttpError(nameof(MercadoPagoProvider),
                nameof(PayAsync), ex.StatusCode, ex);

            return PaymentResult.WithError(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogGenericError(nameof(MercadoPagoProvider),
                nameof(PayAsync), ex);

            return PaymentResult.WithError(ex.Message);
        }
    }
}
