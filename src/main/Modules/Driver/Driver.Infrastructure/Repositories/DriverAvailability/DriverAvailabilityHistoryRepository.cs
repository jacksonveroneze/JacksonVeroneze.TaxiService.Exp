using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Repositories;
using TaxiService.Modules.Driver.Infrastructure.Contexts;

namespace TaxiService.Modules.Driver.Infrastructure.Repositories.DriverAvailability;

[ExcludeFromCodeCoverage]
public class DriverAvailabilityHistoryRepository(
    IEfCoreRepository<DriverAvailabilityHistory, DriversDbContext> service)
    : IDriverAvailabilityHistoryRepository
{
    public async Task CreateAsync(
        DriverAvailabilityHistory entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
