using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverLocation;

public interface IDriverLocationWriteRepository
{
    public Task CreateAsync(
        DriverLocationEntity entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        DriverLocationEntity entity,
        CancellationToken cancellationToken);
}