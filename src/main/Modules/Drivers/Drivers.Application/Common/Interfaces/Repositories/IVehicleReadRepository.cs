using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;

public interface IVehicleReadRepository
{
    public Task<bool> ExistsByPlateAsync(
        string plate,
        CancellationToken cancellationToken);

    public Task<bool> ExistVehicleByDriverAsync(
        Guid driverId,
        CancellationToken cancellationToken);

    public Task<VehicleDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
