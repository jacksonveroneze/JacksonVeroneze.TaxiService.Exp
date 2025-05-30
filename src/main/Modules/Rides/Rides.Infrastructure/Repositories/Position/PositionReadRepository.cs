using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories.Position;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Infrastructure.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.Repositories.Position;

[ExcludeFromCodeCoverage]
public class PositionReadRepository(
    IEfCoreRepository<PositionEntity, RidesDbContext> service)
    : IPositionReadRepository
{
    public Task<ICollection<PositionEntity>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken)
    {
        return service.GetAllAsync(
            conf => conf.RideId == rideId, cancellationToken);
    }
}
