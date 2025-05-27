using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;

public interface IRideRepository
{
    public Task<RideEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        RideEntity entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        RideEntity entity,
        CancellationToken cancellationToken);
}
