using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Ride;

public interface IRideWriteRepository
{
    public Task CreateAsync(
        RideEntity entity,
        CancellationToken cancellationToken = default);

    public Task UpdateAsync(
        RideEntity entity,
        CancellationToken cancellationToken = default);
}