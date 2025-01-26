using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.Interfaces.Repositories.Outbox;

public interface IOutboxWriteRepository
{
    public Task CreateAsync(
        OutboxMessageEntity entity,
        CancellationToken cancellationToken = default);

    public Task UpdateAsync(
        OutboxMessageEntity entity,
        CancellationToken cancellationToken = default);

    public Task<ICollection<OutboxMessageEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);

    public Task<OutboxMessageEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}