using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Outbox;

public interface IOutboxWriteRepository
{
    public Task CreateAsync(
        OutboxMessageEntity entity,
        CancellationToken cancellationToken);
}
