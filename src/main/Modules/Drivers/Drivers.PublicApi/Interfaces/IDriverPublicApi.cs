using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.PublicApi.Queries;

namespace TaxiService.Modules.Drivers.PublicApi.Interfaces;

public interface IDriverPublicApi
{
    Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<DriverDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<ICollection<DriverDto>> GetAsync(
        PublicApiDriverFilter filter,
        CancellationToken cancellationToken);
}
