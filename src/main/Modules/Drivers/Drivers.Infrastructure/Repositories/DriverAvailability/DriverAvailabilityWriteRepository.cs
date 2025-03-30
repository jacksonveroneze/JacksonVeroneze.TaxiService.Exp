using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.DriverAvailability;

[ExcludeFromCodeCoverage]
public class DriverAvailabilityWriteRepository(
    IEfCoreRepository<DriverAvailabilityEntity, DriversDbContext> service)
    : IDriverAvailabilityWriteRepository
{
    public async Task CreateAsync(
        DriverAvailabilityEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
        
        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        DriverAvailabilityEntity entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}