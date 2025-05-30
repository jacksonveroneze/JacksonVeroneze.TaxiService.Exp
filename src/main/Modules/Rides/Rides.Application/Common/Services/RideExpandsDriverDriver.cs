using TaxiService.Modules.Drivers.PublicApi.v1.Interfaces;
using TaxiService.Modules.Drivers.PublicApi.v1.Models;
using TaxiService.Modules.Drivers.PublicApi.v1.Queries;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Services;
using TaxiService.Modules.Rides.Application.v1.Ride;

namespace TaxiService.Modules.Rides.Application.Common.Services;

public class RideExpandsDriverDriver(
    IDriverPublicApi driverPublicApi) : IRideExpandsStrategy
{
    public RideExpands Name { get; set; } = RideExpands.Driver;

    public async Task ApplyAsync(
        ICollection<RideResponse> rides,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(rides);

       Guid[] ids = rides.Select(item => item.DriverId)
            .Where(item => item != null)
            .Select(item => item!.Value)
            .Distinct()
            .ToArray();

       if (ids.Length == 0)
       {
           return;
       }
       
       DriverPublicApiFilter filter = new(ids);
       
        ICollection<DriverPublicApiDto> drivers = await driverPublicApi
            .GetAsync(filter, cancellationToken);

        Dictionary<Guid, DriverPublicApiDto> dict = drivers
            .ToDictionary(key => key.Id, value => value);

        foreach (RideResponse ride in rides)
        {
            if (ride.DriverId is not null 
                && dict.TryGetValue(ride.DriverId!.Value,
                    out DriverPublicApiDto? driver))
            {
                ride.Driver = driver;
            }
        }
    }
}
