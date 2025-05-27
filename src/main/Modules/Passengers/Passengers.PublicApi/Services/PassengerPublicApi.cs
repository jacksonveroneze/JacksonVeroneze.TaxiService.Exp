using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Domain.Filters;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Passengers.PublicApi.Queries;

namespace TaxiService.Modules.Passengers.PublicApi.Services;

public class PassengerPublicApi(
    IPassengerReadRepository repository)
    : IPassengerPublicApi
{
    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return repository
            .ExistsByIdAsync(id, cancellationToken);
    }

    public Task<PassengerDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return repository
            .GetByIdAsync(id, cancellationToken);
    }

    public Task<ICollection<PassengerDto>> GetAsync(
        PublicApiPassengerFilter filter,
        CancellationToken cancellationToken)
    {
        PassengerFilter queryFilter = new()
        {
            Ids = filter.Ids,
        };

        return repository
            .GetAsync(queryFilter, cancellationToken);
    }
}
