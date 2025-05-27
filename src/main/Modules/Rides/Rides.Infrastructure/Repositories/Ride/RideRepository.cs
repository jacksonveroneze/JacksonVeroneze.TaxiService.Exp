using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Infrastructure.Contexts;

namespace TaxiService.Modules.Rides.Infrastructure.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideRepository(
    IEfCoreRepository<RideEntity, RidesDbContext> service)
    : IRideRepository
{
    public async Task<RideEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        RideEntity? result = await service.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);

        return result;
    }

    public async Task CreateAsync(RideEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RideEntity entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
