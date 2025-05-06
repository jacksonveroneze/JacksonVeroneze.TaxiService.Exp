using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Vehicle;

public interface IVehicleRepository
{
    public Task<VehicleEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        VehicleEntity entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        VehicleEntity entity,
        CancellationToken cancellationToken);
}
