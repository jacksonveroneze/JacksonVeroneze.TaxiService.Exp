using TaxiService.Modules.Ride.Application.v1.Ride.Common.Enums;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Ride.Application.Common.Interfaces.Services;

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
