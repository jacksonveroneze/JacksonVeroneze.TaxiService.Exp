using TaxiService.Modules.Rides.Application.v1.Ride;
using TaxiService.Modules.Rides.Application.v1.Ride.Common;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Enums;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Rides.Application.Common.Interfaces.Services;

public interface IRideExpandsStrategy
{
    public RideExpands Name { get; set; }

    Task ApplyAsync(
        ICollection<RideResponse> rides,
        CancellationToken cancellationToken = default);
}
