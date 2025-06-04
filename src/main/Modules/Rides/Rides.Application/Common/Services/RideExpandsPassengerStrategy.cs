using TaxiService.Modules.Passengers.PublicApi.v1.Interfaces;
using TaxiService.Modules.Passengers.PublicApi.v1.Models;
using TaxiService.Modules.Passengers.PublicApi.v1.Queries;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Services;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Enums;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Rides.Application.Common.Services;

public class RideExpandsPassengerStrategy(
    IPassengerPublicApi passengerPublicApi) : IRideExpandsStrategy
{
    public RideExpands Name { get; set; } = RideExpands.Passenger;

    public async Task ApplyAsync(
        ICollection<RideResponse> rides,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(rides);

        Guid[] ids = rides.Select(item => item.PassengerId)
            .Where(item => item != null)
            .Select(item => item!.Value)
            .Distinct()
            .ToArray();

        if (ids.Length == 0)
        {
            return;
        }

        PassengerPublicApiFilter filter = new(ids);

        ICollection<PassengerPublicApiDto> passengers = await passengerPublicApi
            .GetAsync(filter, cancellationToken);

        Dictionary<Guid, PassengerPublicApiDto> dict = passengers
            .ToDictionary(key => key.Id, value => value);

        foreach (RideResponse ride in rides)
        {
            if (ride.PassengerId is not null
                && dict.TryGetValue(ride.PassengerId!.Value,
                    out PassengerPublicApiDto? passenger))
            {
                ride.Passenger = passenger;
            }
        }
    }
}
