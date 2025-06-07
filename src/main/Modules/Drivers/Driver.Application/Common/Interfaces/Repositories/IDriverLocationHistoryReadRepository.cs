using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Models;

namespace TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;

public interface IDriverLocationHistoryReadRepository
{
    public Task<ICollection<DriverLocationMatchDto>> GetDriverNeabyLocationAsync(
        DriverLocationMatchFilter filter,
        CancellationToken cancellationToken);
}
