using TaxiService.Modules.Payment.Domain.Models;
using TaxiService.Modules.Payment.Infrastructure.Acl.Models.MercadoPago;

namespace TaxiService.Modules.Payment.Infrastructure.Acl.Translators;

public class MercadoPagoTranslator
{
    private const string SuccessId = "SUCCESS";

    public PayRequest TranslateToProvider(
        Domain.Aggregates.Payment payment)
    {
        ArgumentNullException.ThrowIfNull(payment);

        return new PayRequest();
    }

    public PaymentResult TranslateFromProvider(
        PayResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);

        bool isSuccess = string.Equals(response.Status,
            SuccessId, StringComparison.OrdinalIgnoreCase);

        return PaymentResult.Build(
            isSuccess,
            response.SignId,
            response.ErrorDescription
        );
    }
}
