using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Ride;

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