using TaxiService.Modules.Rides.Application.v1.Ride.Common.Enums;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Rides.Application.Common.Interfaces.Services;

public interface IRideExpandsService
{
    Task ApplyAsync(
        RideResponse ride,
        ICollection<RideExpands>? expands,
        CancellationToken cancellationToken = default);

    Task ApplyAsync(
        ICollection<RideResponse> rides,
        ICollection<RideExpands>? expands,
        CancellationToken cancellationToken = default);
}
