using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.PublicApi.Interfaces;

namespace TaxiService.Modules.Drivers.PublicApi.Services;

public class DriverService(
    IDriverReadRepository driverReadRepository) : IDriverService
{
    public async Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        bool exists = await driverReadRepository
            .ExistsByIdAsync(id, cancellationToken);

        return exists;
    }
}
