using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.Vehicle;

[ExcludeFromCodeCoverage]
public class VehicleReadRepository(
    IEfCoreRepository<VehicleDto, DriversDbContext> service)
    : IVehicleReadRepository
{
    public Task<bool> ExistsByPlateAsync(
        string plate,
        CancellationToken cancellationToken)
    {
        return service.AnyAsync(conf => conf.Plate == plate,
            cancellationToken);
    }

    public Task<bool> ExistVehicleByDriverAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        return service.AnyAsync(conf => conf.DriverId == driverId,
            cancellationToken);
    }

    public Task<VehicleDto?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken)
    {
        return service.GetSingleOrDefaultAsync(
            conf => conf.DriverId == driverId,
            cancellationToken);
    }

    public Task<VehicleDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(
            conf => conf.Id == id,
            cancellationToken);
    }
}
