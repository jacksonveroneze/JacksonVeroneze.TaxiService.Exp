using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;

public interface IDriverWriteRepository
{
    public Task CreateAsync(
        DriverEntity entity,
        CancellationToken cancellationToken = default);

    public Task UpdateAsync(
        DriverEntity entity,
        CancellationToken cancellationToken = default);
}