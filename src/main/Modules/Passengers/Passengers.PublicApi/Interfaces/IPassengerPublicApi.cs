using TaxiService.Modules.Passengers.Application.v1.Passenger;
using TaxiService.Modules.Passengers.PublicApi.Queries;

namespace TaxiService.Modules.Passengers.PublicApi.Interfaces;

public interface IPassengerPublicApi
{
    Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
    
    Task<PassengerDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<ICollection<PassengerDto>> GetAsync(
        PublicApiPassengerFilter filter,
        CancellationToken cancellationToken);
}
