using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories;

public interface IOutboxWriteRepository
{
    public Task CreateAsync(
        OutboxMessage entity,
        CancellationToken cancellationToken);
}
