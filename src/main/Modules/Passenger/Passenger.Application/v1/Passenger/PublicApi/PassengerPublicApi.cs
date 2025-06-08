using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passenger.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Models;
using TaxiService.Modules.Passenger.PublicApi.v1.Interfaces;
using TaxiService.Modules.Passenger.PublicApi.v1.Models;
using TaxiService.Modules.Passenger.PublicApi.v1.Queries;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.PublicApi;

public class PassengerPublicApi(
    IDataMapper mapper,
    IPassengerReadRepository repository)
    : IPassengerPublicApi
{
    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return repository.ExistsByIdAsync(
            id, cancellationToken);
    }

    public async Task<PassengerPublicApiDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassengerDto? data = await repository
            .GetByIdAsync(id, cancellationToken);

        return mapper.Map<PassengerPublicApiDto>(data);
    }

    public async Task<ICollection<PassengerPublicApiDto>> GetAsync(
        PassengerPublicApiFilter filter,
        CancellationToken cancellationToken)
    {
        PassengerFilter queryFilter = new()
        {
            Ids = filter.Ids,
        };

        ICollection<PassengerDto> data = await repository
            .GetAsync(queryFilter, cancellationToken);

        return mapper.Map<ICollection<PassengerPublicApiDto>>(data);
    }
}
