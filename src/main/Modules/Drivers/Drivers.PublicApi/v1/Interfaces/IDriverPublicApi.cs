using TaxiService.Modules.Drivers.PublicApi.v1.Models;
using TaxiService.Modules.Drivers.PublicApi.v1.Queries;

namespace TaxiService.Modules.Drivers.PublicApi.v1.Interfaces;

public interface IDriverPublicApi
{
    Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<DriverPublicApiDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<ICollection<DriverPublicApiDto>> GetAsync(
        DriverPublicApiFilter filter,
        CancellationToken cancellationToken);
}
