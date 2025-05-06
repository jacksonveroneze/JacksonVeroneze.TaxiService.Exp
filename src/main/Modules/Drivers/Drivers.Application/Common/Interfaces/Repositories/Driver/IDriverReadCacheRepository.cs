using TaxiService.Modules.Drivers.Application.v1.Driver.Common;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;

public interface IDriverReadCacheRepository
{
    public Task<DriverDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task RemoveAsync(
        Guid id,
        CancellationToken cancellationToken);
}
