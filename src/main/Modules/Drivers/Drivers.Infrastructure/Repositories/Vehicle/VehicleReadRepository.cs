using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Specifications.Driver;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.Vehicle;

[ExcludeFromCodeCoverage]
public class VehicleReadRepository(
    IEfCoreRepository<VehicleEntity, DriversDbContext> service)
    : IVehicleReadRepository
{
    public Task<bool> ExistsByPlateAsync(
        string plate,
        CancellationToken cancellationToken)
    {
        return service.AnyAsync(conf => conf.Plate == plate,
            cancellationToken);
    }
    
    public Task<VehicleEntity?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        return service.GetSingleOrDefaultAsync(
            conf => conf.DriverId == driverId,
            cancellationToken);
    }
}