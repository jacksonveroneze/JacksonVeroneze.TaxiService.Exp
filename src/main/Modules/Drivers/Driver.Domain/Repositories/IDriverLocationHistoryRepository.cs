using TaxiService.Modules.Driver.Domain.Entities;

namespace TaxiService.Modules.Driver.Domain.Repositories;

public interface IDriverLocationHistoryRepository
{
    public Task CreateAsync(
        DriverLocationHistory entity,
        CancellationToken cancellationToken);
}
