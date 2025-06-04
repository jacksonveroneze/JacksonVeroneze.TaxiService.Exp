using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Domain.Repositories;

public interface IDriverLocationHistoryRepository
{
    public Task CreateAsync(
        DriverLocationHistory entity,
        CancellationToken cancellationToken);
}
