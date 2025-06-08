using TaxiService.Modules.Payment.Domain.Enums;
using TaxiService.Modules.Payment.Domain.Models;

namespace TaxiService.Modules.Payment.Application.Common.Interfaces.Providers;

public interface IPaymentProvider
{
    public PaymentProviderType Identifier { get; }

    Task<PaymentResult> PayAsync(
        Domain.Aggregates.Payment payment,
        CancellationToken cancellationToken);
}
