using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Position;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Specifications.Position;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Position;

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