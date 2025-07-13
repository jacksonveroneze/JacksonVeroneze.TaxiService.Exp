using TaxiService.Common.SharedKernel.Interfaces;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Driver.PublicApi.v1.Interfaces;
using TaxiService.Modules.Driver.PublicApi.v1.Models;
using TaxiService.Modules.Driver.PublicApi.v1.Queries;
using IDriverReadRepository =
    TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories.IDriverReadRepository;

namespace TaxiService.Modules.Driver.Application.v1.Driver.PublicApi;

public class DriverPublicApi(
    IDataMapper mapper,
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
            Status = filter.Status,
            
        };

        ICollection<DriverDto> data = await repository
            .GetAsync(queryFilter, cancellationToken);

        return mapper.Map<ICollection<DriverPublicApiDto>>(data);
    }
}
