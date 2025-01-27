using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Position;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Specifications.Position;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Repositories.Position;

[ExcludeFromCodeCoverage]
public class PositionReadRepository(
    IEfCoreRepository<PositionEntity> service)
    : IPositionReadRepository
{
    public async Task<ICollection<PositionEntity>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default)
    {
        PositionByRideIdSpecification spec = new(rideId);

        return await service
            .GetAllAsync(spec.ToExpression(), cancellationToken);
    }
}