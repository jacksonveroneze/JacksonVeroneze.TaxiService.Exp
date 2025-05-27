using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Outbox;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Repositories.Outbox;

[ExcludeFromCodeCoverage]
public class OutboxWriteWriteRepository(
    IEfCoreRepository<OutboxMessageEntity, PassengersDbContext> service)
    : IOutboxWriteRepository
{
    public async Task CreateAsync(OutboxMessageEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }
}