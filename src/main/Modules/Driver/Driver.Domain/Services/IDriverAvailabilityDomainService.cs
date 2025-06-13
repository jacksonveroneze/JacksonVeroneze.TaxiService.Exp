using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Domain.Services;

public interface IDriverAvailabilityDomainService
{
    Result<DriverAvailabilityHistory> TryUpdateAvailabilityStatus(
        Guid driverId,
        DriverAvailabilityStatus? currentStatus,
        DriverAvailabilityStatus newStatus);
}
