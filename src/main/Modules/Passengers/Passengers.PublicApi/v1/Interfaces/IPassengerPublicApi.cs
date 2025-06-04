using TaxiService.Modules.Passengers.PublicApi.v1.Models;
using TaxiService.Modules.Passengers.PublicApi.v1.Queries;

namespace TaxiService.Modules.Passengers.PublicApi.v1.Interfaces;

public interface IPassengerPublicApi
{
    Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<PassengerPublicApiDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<ICollection<PassengerPublicApiDto>> GetAsync(
        PassengerPublicApiFilter filter,
        CancellationToken cancellationToken);
}
