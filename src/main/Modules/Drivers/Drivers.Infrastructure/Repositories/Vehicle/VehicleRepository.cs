using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.Vehicle;

[ExcludeFromCodeCoverage]
public class VehicleRepository(
    IEfCoreRepository<VehicleEntity, DriversDbContext> service)
    : IVehicleRepository
{
    public Task<VehicleEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);
    }

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
