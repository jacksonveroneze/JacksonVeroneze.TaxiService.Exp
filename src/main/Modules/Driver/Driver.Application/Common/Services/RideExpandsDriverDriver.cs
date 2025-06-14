using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Services;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Enums;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Driver.Application.v1.DriverAvailability.Models;
using TaxiService.Modules.Driver.Domain.Entities;

namespace TaxiService.Modules.Driver.Application.Common.Services;

public class DriverExpandsDriverAvailability(
    IDriverAvailabilityHistoryReadRepository driverAvailabilityHistoryReadRepository)
    : IDriverExpandsStrategy
{
    public DriverExpands Name { get; set; } = DriverExpands.Availability;

    public async Task ApplyAsync(
        ICollection<DriverResponse> drivers,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(drivers);

        Guid[] ids = drivers.Select(item => item.Id)
            .Distinct()
            .ToArray();

        if (ids.Length == 0)
        {
            return;
        }

        ICollection<DriverAvailabilityDto>? driverAvailabilities =
            await driverAvailabilityHistoryReadRepository
                .GetLastByDriverIdsAsync(ids, cancellationToken);
        
        Dictionary<Guid, DriverAvailabilityDto> dict = driverAvailabilities?
            .ToDictionary(key => key.DriverId, value => value) ?? [];

        foreach (DriverResponse driver in drivers)
        {
            if (dict.TryGetValue(driver.Id,
                    out DriverAvailabilityDto? driverAvailability))
            {
                driver.Availability = new DriverAvailabilityResponse
                {
                    Status = driverAvailability.Status,
                };
            }
        }
    }
}
