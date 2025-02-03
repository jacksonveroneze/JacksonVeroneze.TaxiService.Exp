using TaxiService.Modules.Payments.Domain.Entities;
using TaxiService.Modules.Payments.Domain.Models;
using TaxiService.Modules.Payments.Infrastructure.Acl.Models.Stripe;

namespace TaxiService.Modules.Payments.Infrastructure.Acl.Translators;

public class StripeTranslator
{
    private const string SuccessId = "SUCCESS";

    public PayRequest TranslateToProvider(
        PaymentEntity payment)
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