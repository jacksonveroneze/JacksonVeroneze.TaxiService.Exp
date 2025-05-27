using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;

namespace TaxiService.Modules.Rides.Application.v1.Interfaces.Services;

public interface IRideExpandsStrategy
{
    public RideExpands Name { get; set; }

    Task ApplyAsync(
        ICollection<RideResponse> rides,
        CancellationToken cancellationToken = default);
}
