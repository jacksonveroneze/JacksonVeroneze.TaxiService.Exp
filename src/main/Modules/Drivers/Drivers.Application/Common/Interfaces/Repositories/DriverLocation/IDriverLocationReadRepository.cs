using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.DriverLocation;

public interface IDriverLocationReadRepository
{
    public Task<DriverLocationEntity?> GetByDriverIdAsync(
        Guid driverId,
        CancellationToken cancellationToken);
}