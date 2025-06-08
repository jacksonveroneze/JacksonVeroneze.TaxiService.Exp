using TaxiService.Modules.Ride.Application.v1.Ride.Common.Enums;
using TaxiService.Modules.Ride.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Ride.Application.Common.Interfaces.Services;

public interface IRideExpandsStrategy
{
    public RideExpands Name { get; set; }

    Task ApplyAsync(
        ICollection<RideResponse> rides,
        CancellationToken cancellationToken = default);
}
