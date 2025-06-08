namespace TaxiService.Modules.Ride.Domain.Repositories;

public interface IRideRepository
{
    public Task<Aggregates.Ride?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        Aggregates.Ride entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        Aggregates.Ride entity,
        CancellationToken cancellationToken);
}
