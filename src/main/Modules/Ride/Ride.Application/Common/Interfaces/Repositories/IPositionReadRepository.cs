using TaxiService.Modules.Ride.Domain.Entities;

namespace TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;

public interface IPositionReadRepository
{
    public Task<ICollection<Position>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken);
}
