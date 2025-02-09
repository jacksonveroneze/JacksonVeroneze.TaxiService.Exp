using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Infrastructure.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideWriteRepository(
    IEfCoreRepository<RideEntity, RidesDbContext> service) : IRideWriteRepository
{
    public async Task CreateAsync(RideEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RideEntity entity,
        CancellationToken cancellationToken)
    {
        // entity.MarkAsUpdated();
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}