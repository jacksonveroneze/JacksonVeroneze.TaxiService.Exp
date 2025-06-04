using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Rides.Domain.Repositories;
using TaxiService.Modules.Rides.Infrastructure.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.Repositories.Position;

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
