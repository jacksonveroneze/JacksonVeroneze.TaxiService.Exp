using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Repositories;
using TaxiService.Modules.Driver.Infrastructure.Contexts;

namespace TaxiService.Modules.Driver.Infrastructure.Repositories.DriverLocation;

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
