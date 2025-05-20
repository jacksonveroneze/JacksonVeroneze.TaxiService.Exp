using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;

public interface IPassengerWriteRepository
{
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
