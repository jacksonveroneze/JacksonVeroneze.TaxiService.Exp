using TaxiService.Modules.Payment.Application.Common.Interfaces.Repositories;

namespace TaxiService.Modules.Payment.Infrastructure.Repositories.Payment;

[ExcludeFromCodeCoverage]
public class PaymentReadRepository()
    : IPaymentReadRepository
{
    public Task<ICollection<Domain.Aggregates.Payment>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }
}
