using TaxiService.Modules.Passengers.Domain.Aggregates;

namespace TaxiService.Modules.Passengers.Domain.Repositories;

public interface IPassengerRepository
{
    public Task<Passenger?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        Passenger entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        Passenger entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        Passenger entity,
        CancellationToken cancellationToken);
}
