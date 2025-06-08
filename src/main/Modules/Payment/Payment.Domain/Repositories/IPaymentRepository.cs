namespace TaxiService.Modules.Payment.Domain.Repositories;

public interface IPaymentRepository
{
    public Task<ICollection<Domain.Aggregates.Payment>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken);
    
    public Task CreateAsync(
        Domain.Aggregates.Payment entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        Domain.Aggregates.Payment entity,
        CancellationToken cancellationToken);
}
