using TaxiService.Modules.Passengers.Application.v1.Passenger;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;
using TaxiService.Modules.Passengers.PublicApi.Queries;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Services;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;
using TaxiService.Modules.Rides.Application.v1.Ride;

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
        
        Guid?[] ids = rides.Select(item => item.PassengerId)
            .Distinct()
            .Where(item => item != null)
            .ToArray();

        if (ids.Length == 0)
        {
            return;
        }
        
        PublicApiPassengerFilter passengerFilter = new(ids);
        
        ICollection<PassengerDto> drivers = await passengerPublicApi
            .GetAsync(passengerFilter, cancellationToken);

        Dictionary<Guid, PassengerDto> dict = drivers
            .ToDictionary(key => key.Id!.Value, value => value);
        
        foreach (RideResponse ride in rides)
        {
            if (dict.TryGetValue(ride.PassengerId!.Value,
                    out PassengerDto? passenger))
            {
                ride.Passenger = passenger;
            }
        }
    }
}
