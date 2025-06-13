using TaxiService.Modules.Driver.Domain.Entities;

namespace TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;

public interface IDriverAvailabilityHistoryReadRepository
{
    public Task<DriverAvailabilityHistory?> GetLastByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken);
    
    public Task<ICollection<DriverAvailabilityHistory>> GetLastByDriverIdsAsync(
        Guid[] driverIds,
        CancellationToken cancellationToken);
}
