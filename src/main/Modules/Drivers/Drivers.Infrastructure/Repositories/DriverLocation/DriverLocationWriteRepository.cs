using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverLocation;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverLocation;

[ExcludeFromCodeCoverage]
public class DriverLocationWriteRepository(
    IEfCoreRepository<DriverLocationEntity, DriversDbContext> service)
    : IDriverLocationWriteRepository
{
    public async Task CreateAsync(
        DriverLocationEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
        
        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        DriverLocationEntity entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}