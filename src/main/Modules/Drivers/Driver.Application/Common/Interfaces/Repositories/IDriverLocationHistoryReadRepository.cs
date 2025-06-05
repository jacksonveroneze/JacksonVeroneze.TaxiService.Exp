using TaxiService.Modules.Driver.Domain.Entities;

namespace TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;

public interface IDriverLocationHistoryReadRepository
{
    public Task<DriverLocationHistory?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken);
}
