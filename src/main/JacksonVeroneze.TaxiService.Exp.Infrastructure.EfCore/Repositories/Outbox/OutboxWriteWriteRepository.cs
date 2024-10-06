using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Repositories.Outbox;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Outbox;

[ExcludeFromCodeCoverage]
public class OutboxWriteWriteRepository(
    IEfCoreRepository<OutboxMessageEntity> service) : IOutboxWriteRepository
{
    public async Task CreateAsync(OutboxMessageEntity entity,
        CancellationToken cancellationToken = default)
    {
        await service.CreateAsync(entity, cancellationToken);
    }
}