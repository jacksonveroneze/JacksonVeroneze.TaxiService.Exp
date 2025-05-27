using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Filters;
using TaxiService.Modules.Drivers.PublicApi.Interfaces;
using TaxiService.Modules.Drivers.PublicApi.Queries;

namespace TaxiService.Modules.Drivers.PublicApi.Services;

public class DriverPublicApi(
    IDriverReadRepository repository)
    : IDriverPublicApi
{
    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return repository
            .ExistsByIdAsync(id, cancellationToken);
    }

    public Task<DriverDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return repository
            .GetByIdAsync(id, cancellationToken);
    }

    public Task<ICollection<DriverDto>> GetAsync(
        PublicApiDriverFilter filter,
        CancellationToken cancellationToken)
    {
        DriverFilter queryFilter = new()
        {
            Ids = filter.Ids,
        };

        return repository
            .GetAsync(queryFilter, cancellationToken);
    }
}
