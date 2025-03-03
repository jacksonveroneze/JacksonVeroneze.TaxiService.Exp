using TaxiService.Modules.Drivers.Application.v1.DTOs;

namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;

public interface IDriverReadCacheRepository
{
    public Task<DriverDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}