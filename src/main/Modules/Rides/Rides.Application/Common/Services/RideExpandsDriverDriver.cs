using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.PublicApi.Interfaces;
using TaxiService.Modules.Drivers.PublicApi.Queries;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Services;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;
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

       Guid?[] ids = rides.Select(item => item.DriverId)
            .Distinct()
            .Where(item => item != null)
            .ToArray();

       if (ids.Length == 0)
       {
           return;
       }
       
       PublicApiDriverFilter driverFilter = new(ids);
       
        ICollection<DriverDto> drivers = await driverPublicApi
            .GetAsync(driverFilter, cancellationToken);

        Dictionary<Guid, DriverDto> dict = drivers
            .ToDictionary(key => key.Id!.Value, value => value);

        foreach (RideResponse ride in rides)
        {
            if (dict.TryGetValue(ride.DriverId!.Value,
                    out DriverDto? driver))
            {
                ride.Driver = driver;
            }
        }
    }
}
