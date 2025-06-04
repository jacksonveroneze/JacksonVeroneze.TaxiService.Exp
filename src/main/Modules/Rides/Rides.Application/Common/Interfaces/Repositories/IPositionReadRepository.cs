namespace TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories;

public interface IPositionReadRepository
{
    public Task<ICollection<Domain.Entities.Position>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken);
}
