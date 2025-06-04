using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories;

public interface IPositionReadRepository
{
    public Task<ICollection<Position>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken);
}
