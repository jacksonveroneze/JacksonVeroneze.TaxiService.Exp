using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories;

public interface IDriverAvailabilityHistoryReadRepository
{
    public Task<DriverAvailabilityHistory?> GetLastByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken);
}
