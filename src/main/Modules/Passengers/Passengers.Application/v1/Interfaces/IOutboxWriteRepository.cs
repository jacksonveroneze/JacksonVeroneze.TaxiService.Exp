using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces;

public interface IOutboxWriteRepository
{
    public Task CreateAsync(
        OutboxMessageEntity entity,
        CancellationToken cancellationToken);
}