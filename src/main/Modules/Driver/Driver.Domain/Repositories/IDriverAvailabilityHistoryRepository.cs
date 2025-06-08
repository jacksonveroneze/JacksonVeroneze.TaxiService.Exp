using TaxiService.Modules.Driver.Domain.Entities;

namespace TaxiService.Modules.Driver.Domain.Repositories;

public interface IDriverAvailabilityHistoryRepository
{
    public Task CreateAsync(
        DriverAvailabilityHistory entity,
        CancellationToken cancellationToken);
}
