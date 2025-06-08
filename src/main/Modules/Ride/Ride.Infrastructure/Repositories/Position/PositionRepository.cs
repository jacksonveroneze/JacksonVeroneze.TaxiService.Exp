using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Ride.Domain.Repositories;
using TaxiService.Modules.Ride.Infrastructure.Contexts;

namespace TaxiService.Modules.Ride.Infrastructure.Repositories.Position;

[ExcludeFromCodeCoverage]
public class PositionRepository(
    IEfCoreRepository<Domain.Entities.Position, RidesDbContext> service)
    : IPositionRepository
{
    public async Task CreateAsync(Domain.Entities.Position entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
