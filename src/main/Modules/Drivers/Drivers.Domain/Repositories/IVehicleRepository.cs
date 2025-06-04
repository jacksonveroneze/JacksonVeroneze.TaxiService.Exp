using TaxiService.Modules.Drivers.Domain.Aggregates;

namespace TaxiService.Modules.Drivers.Domain.Repositories;

public interface IVehicleRepository
{
    public Task<Vehicle?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        Vehicle entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        Vehicle entity,
        CancellationToken cancellationToken);
}
