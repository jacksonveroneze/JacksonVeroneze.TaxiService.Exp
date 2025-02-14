using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.DriverAvailability;

public interface IDriverAvailabilityWriteRepository
{
    public Task CreateAsync(
        DriverAvailabilityEntity entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        DriverAvailabilityEntity entity,
        CancellationToken cancellationToken);
}