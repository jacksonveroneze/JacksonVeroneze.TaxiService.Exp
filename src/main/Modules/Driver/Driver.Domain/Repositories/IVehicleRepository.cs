using TaxiService.Modules.Driver.Domain.Aggregates;

namespace TaxiService.Modules.Driver.Domain.Repositories;

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
