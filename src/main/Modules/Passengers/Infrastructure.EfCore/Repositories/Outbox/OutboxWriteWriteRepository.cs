using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passengers.Application.Interfaces.Repositories.Outbox;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Repositories.Outbox;

[ExcludeFromCodeCoverage]
public class OutboxWriteWriteRepository(
    IEfCoreRepository<OutboxMessageEntity> service) : IOutboxWriteRepository
{
    public async Task CreateAsync(OutboxMessageEntity entity,
        CancellationToken cancellationToken = default)
    {
        await service.CreateAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(OutboxMessageEntity entity,
        CancellationToken cancellationToken = default)
    {
        service.Update(entity);

        await service.UnitOfWork.CommitAsync(cancellationToken);
    }

    public Task<ICollection<OutboxMessageEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return service.GetAllAsync(x => x.Id != null,
            cancellationToken: cancellationToken);
    }

    public Task<OutboxMessageEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return service.GetByIdAsync(x => x.Id == id,
            cancellationToken);
    }
}