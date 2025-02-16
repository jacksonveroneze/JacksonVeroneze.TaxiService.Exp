using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.Vehicle;

[ExcludeFromCodeCoverage]
public class VehicleWriteRepository(
    IEfCoreRepository<VehicleEntity, DriversDbContext> service) 
    : IVehicleWriteRepository
{
    public async Task CreateAsync(VehicleEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(VehicleEntity entity,
        CancellationToken cancellationToken)
    {
        service.Update(entity);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }
}