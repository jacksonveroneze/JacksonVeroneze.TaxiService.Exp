using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Filters;
using TaxiService.Modules.Passengers.PublicApi.v1.Interfaces;
using TaxiService.Modules.Passengers.PublicApi.v1.Models;
using TaxiService.Modules.Passengers.PublicApi.v1.Queries;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.PublicApi;

public class PassengerPublicApi(
    IMapper mapper,
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
