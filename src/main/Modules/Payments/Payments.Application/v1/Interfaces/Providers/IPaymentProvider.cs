using TaxiService.Modules.Payments.Domain.Entities;
using TaxiService.Modules.Payments.Domain.Enums;
using TaxiService.Modules.Payments.Domain.Models;

namespace TaxiService.Modules.Payments.Application.v1.Interfaces.Providers;

public interface IPaymentProvider
{
    public PaymentProviderType Identifier { get; }

    Task<PaymentResult> PayAsync(
        PaymentEntity payment,
        CancellationToken cancellationToken);
}