namespace TaxiService.Modules.Payment.Domain.Repositories;

public interface IPaymentRepository
{
    public Task<ICollection<Aggregates.Payment>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken);
    
    public Task CreateAsync(
        Aggregates.Payment entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        Aggregates.Payment entity,
        CancellationToken cancellationToken);
}
