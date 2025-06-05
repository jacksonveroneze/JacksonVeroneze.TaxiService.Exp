using TaxiService.Modules.Passenger.Domain.Entities;

namespace TaxiService.Modules.Passenger.Application.Common.Interfaces.Repositories;

public interface IOutboxWriteRepository
{
    public Task CreateAsync(
        OutboxMessage entity,
        CancellationToken cancellationToken);
}
