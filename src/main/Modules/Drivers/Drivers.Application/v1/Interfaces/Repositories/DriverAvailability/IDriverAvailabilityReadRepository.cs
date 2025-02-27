using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.DriverAvailability;

public interface IDriverAvailabilityReadRepository
{
    public Task<DriverAvailabilityEntity?> GetLastByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken);
}