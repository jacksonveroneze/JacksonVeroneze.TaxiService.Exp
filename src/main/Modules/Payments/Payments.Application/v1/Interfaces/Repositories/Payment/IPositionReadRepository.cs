using TaxiService.Modules.Payments.Domain.Entities;

namespace TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Payment;

public interface IPaymentReadRepository
{
    public Task<ICollection<PaymentEntity>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default);
}
