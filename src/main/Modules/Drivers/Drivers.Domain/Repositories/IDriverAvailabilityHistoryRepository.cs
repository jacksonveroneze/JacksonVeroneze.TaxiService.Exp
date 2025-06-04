using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Domain.Repositories;

public interface IDriverAvailabilityHistoryRepository
{
    public Task CreateAsync(
        DriverAvailabilityHistory entity,
        CancellationToken cancellationToken);
}
