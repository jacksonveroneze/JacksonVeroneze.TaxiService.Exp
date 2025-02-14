using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Vehicle;

public interface IVehicleReadRepository
{
    public Task<VehicleEntity?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken);
}