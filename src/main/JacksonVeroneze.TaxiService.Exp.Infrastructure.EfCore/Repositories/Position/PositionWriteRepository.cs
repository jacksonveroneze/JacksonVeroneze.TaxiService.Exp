using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Position;

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