using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Repositories;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverLocation;

[ExcludeFromCodeCoverage]
public class DriverLocationHistoryRepository(
    IEfCoreRepository<DriverLocationHistory, DriversDbContext> service)
    : IDriverLocationHistoryRepository
{
    public async Task CreateAsync(
        DriverLocationHistory entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}
