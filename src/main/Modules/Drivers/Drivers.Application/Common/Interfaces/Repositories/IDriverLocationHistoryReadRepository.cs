using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;

public interface IDriverLocationHistoryReadRepository
{
    public Task<DriverLocationHistory?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken);
}
