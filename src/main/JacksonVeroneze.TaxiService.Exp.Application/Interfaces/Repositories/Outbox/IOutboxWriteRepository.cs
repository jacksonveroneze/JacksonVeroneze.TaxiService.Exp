using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Repositories.Outbox;

public interface IOutboxWriteRepository
{
    Task CreateAsync(
        OutboxMessageEntity entity,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        OutboxMessageEntity entity,
        CancellationToken cancellationToken = default);

    Task<ICollection<OutboxMessageEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<OutboxMessageEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}