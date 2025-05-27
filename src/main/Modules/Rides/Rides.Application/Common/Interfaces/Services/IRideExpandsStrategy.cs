using TaxiService.Modules.Rides.Application.v1.Queries.Ride;
using TaxiService.Modules.Rides.Application.v1.Ride;

namespace TaxiService.Modules.Rides.Application.Common.Interfaces.Services;

public interface IRideExpandsStrategy
{
    public RideExpands Name { get; set; }

    Task ApplyAsync(
        ICollection<RideResponse> rides,
        CancellationToken cancellationToken = default);
}
