using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Vehicle;

public interface IVehicleReadRepository
{
    public Task<bool> ExistsByPlateAsync(
        string plate,
        CancellationToken cancellationToken);

    public Task<bool> ExistVehicleByDriverAsync(
        Guid driverId,
        CancellationToken cancellationToken);

    public Task<VehicleEntity?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken);
}