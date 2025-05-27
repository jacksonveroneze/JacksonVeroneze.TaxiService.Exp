using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;

public interface IPassengerRepository
{
    public Task<PassengerEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        PassengerEntity entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        PassengerEntity entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        PassengerEntity entity,
        CancellationToken cancellationToken);
}
