using TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Payment;
using TaxiService.Modules.Payments.Domain.Entities;

namespace TaxiService.Modules.Payments.Infrastructure.Repositories.Payment;

[ExcludeFromCodeCoverage]
public class PaymentReadRepository()
    : IPaymentReadRepository
{
    public Task<ICollection<PaymentEntity>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }
}
