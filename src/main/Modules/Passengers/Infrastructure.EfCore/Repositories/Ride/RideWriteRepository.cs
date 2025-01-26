using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideWriteRepository(
    IEfCoreRepository<RideEntity> service) : IRideWriteRepository
{
    public async Task CreateAsync(RideEntity entity,
        CancellationToken cancellationToken = default)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.UnitOfWork.CommitAsync(cancellationToken);
    }

    public async Task UpdateAsync(RideEntity entity,
        CancellationToken cancellationToken = default)
    {
        // entity.MarkAsUpdated();
        service.Update(entity);

        await service.UnitOfWork.CommitAsync(cancellationToken);
    }
}