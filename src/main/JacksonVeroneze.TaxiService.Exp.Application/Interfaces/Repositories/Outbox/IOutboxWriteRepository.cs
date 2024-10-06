using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Repositories.Outbox;

public interface IOutboxWriteRepository
{
    Task CreateAsync(
        OutboxMessageEntity entity,
        CancellationToken cancellationToken = default);
}