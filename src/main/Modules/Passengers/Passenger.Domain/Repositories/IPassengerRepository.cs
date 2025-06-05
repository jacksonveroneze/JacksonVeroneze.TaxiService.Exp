namespace TaxiService.Modules.Passenger.Domain.Repositories;

public interface IPassengerRepository
{
    public Task<Aggregates.Passenger?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        Aggregates.Passenger entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        Aggregates.Passenger entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        Aggregates.Passenger entity,
        CancellationToken cancellationToken);
}
