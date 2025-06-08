using TaxiService.Modules.Ride.Application.Common.Interfaces.Services.Models;

namespace TaxiService.Modules.Ride.Application.Common.Interfaces.Services;

public interface IRideMatchDriversService
{
    Task<ICollection<DriverMatchCandidate>> GetMatchDriversAsync(
        RideMatchFilter filter,
        CancellationToken cancellationToken);
}
