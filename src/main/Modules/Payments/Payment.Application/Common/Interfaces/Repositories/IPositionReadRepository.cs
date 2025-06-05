namespace TaxiService.Modules.Payment.Application.Common.Interfaces.Repositories;

public interface IPaymentReadRepository
{
    public Task<ICollection<Domain.Aggregates.Payment>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken);
}
