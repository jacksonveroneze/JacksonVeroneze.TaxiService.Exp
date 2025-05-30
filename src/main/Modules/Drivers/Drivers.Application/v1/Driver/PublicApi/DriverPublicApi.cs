using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Filters;
using TaxiService.Modules.Drivers.PublicApi.v1.Interfaces;
using TaxiService.Modules.Drivers.PublicApi.v1.Models;
using TaxiService.Modules.Drivers.PublicApi.v1.Queries;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.PublicApi;

public class DriverPublicApi(
    IMapper mapper,
    IDriverReadRepository repository)
    : IDriverPublicApi
{
    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return repository.ExistsByIdAsync(
            id, cancellationToken);
    }

    public async Task<DriverPublicApiDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        DriverDto? data = await repository
            .GetByIdAsync(id, cancellationToken);

        return mapper.Map<DriverPublicApiDto>(data);
    }

    public async Task<ICollection<DriverPublicApiDto>> GetAsync(
        DriverPublicApiFilter filter,
        CancellationToken cancellationToken)
    {
        DriverFilter queryFilter = new()
        {
            Ids = filter.Ids,
        };

        ICollection<DriverDto> data = await repository
            .GetAsync(queryFilter, cancellationToken);

        return mapper.Map<ICollection<DriverPublicApiDto>>(data);
    }
}
