using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passenger.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passenger.Domain.Entities;
using TaxiService.Modules.Passenger.Infrastructure.Contexts;

namespace TaxiService.Modules.Passenger.Infrastructure.Repositories.Outbox;

[ExcludeFromCodeCoverage]
public class OutboxWriteWriteRepository(
    IEfCoreRepository<OutboxMessage, PassengersDbContext> service)
    : IOutboxWriteRepository
{
    public async Task CreateAsync(OutboxMessage entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }
}
