using TaxiService.Modules.Driver.Application.v1.DriverAvailability.Models;

namespace TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;

public interface IDriverAvailabilityHistoryReadRepository
{
    public Task<DriverAvailabilityDto?> GetLastByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken);
    
    public Task<ICollection<DriverAvailabilityDto>> GetLastByDriverIdsAsync(
        Guid[] driverIds,
        CancellationToken cancellationToken);
}
