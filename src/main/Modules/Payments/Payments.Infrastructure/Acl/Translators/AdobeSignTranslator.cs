using TaxiService.Modules.Payments.Domain.Entities;
using TaxiService.Modules.Payments.Domain.Models;
using TaxiService.Modules.Payments.Infrastructure.Acl.Models.Adobe;

namespace TaxiService.Modules.Payments.Infrastructure.Acl.Translators;

public class AdobeSignTranslator
{
    private const string SuccessId = "SUCCESS";

    public SignRequest TranslateToProvider(
        PaymentEntity payment)
    {
        ArgumentNullException.ThrowIfNull(payment);

        return new SignRequest();
    }

    public PaymentResult TranslateFromProvider(
        SignResponse response)
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