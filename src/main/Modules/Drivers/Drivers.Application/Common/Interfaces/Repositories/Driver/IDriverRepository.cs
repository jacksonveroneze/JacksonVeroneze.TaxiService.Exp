using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;

public interface IDriverRepository
{
    public Task<DriverEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task CreateAsync(
        DriverEntity entity,
        CancellationToken cancellationToken);

    public Task DeleteAsync(
        DriverEntity entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        DriverEntity entity,
        CancellationToken cancellationToken);
}
