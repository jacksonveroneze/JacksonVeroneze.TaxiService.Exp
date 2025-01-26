using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.Interfaces.Repositories.Outbox;

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