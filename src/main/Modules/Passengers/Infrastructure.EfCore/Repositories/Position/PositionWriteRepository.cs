using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Position;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Position;

[ExcludeFromCodeCoverage]
public class PositionWriteRepository(
    IEfCoreRepository<PositionEntity> service)
    : IPositionWriteRepository
{
    public async Task CreateAsync(PositionEntity entity,
        CancellationToken cancellationToken = default)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.UnitOfWork.CommitAsync(cancellationToken);
    }
}