using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Ride.Infrastructure.Contexts;

namespace TaxiService.Modules.Ride.Infrastructure.Repositories.Position;

[ExcludeFromCodeCoverage]
public class PositionReadRepository(
    IEfCoreRepository<Domain.Entities.Position, RidesDbContext> service)
    : IPositionReadRepository
{
    public Task<ICollection<Domain.Entities.Position>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken)
    {
        return service.GetAllAsync(
            conf => conf.RideId == rideId, cancellationToken);
    }
}
