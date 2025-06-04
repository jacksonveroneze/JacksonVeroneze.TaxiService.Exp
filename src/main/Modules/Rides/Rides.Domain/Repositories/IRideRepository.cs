using TaxiService.Modules.Rides.Domain.Aggregates;

namespace TaxiService.Modules.Rides.Domain.Repositories;

public interface IRideRepository
{
    public Task<Ride?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        Ride entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        Ride entity,
        CancellationToken cancellationToken);
}
