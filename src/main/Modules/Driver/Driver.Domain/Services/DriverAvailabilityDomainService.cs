using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Domain.Services;

public sealed class DriverAvailabilityDomainService : IDriverAvailabilityDomainService
{
    private static readonly Dictionary<DriverAvailabilityStatus, DriverAvailabilityStatus[]> ValidTransitions = new()
    {
        [DriverAvailabilityStatus.Offline] =
        [
            DriverAvailabilityStatus.Online,
        ],
        [DriverAvailabilityStatus.Online] =
        [
            DriverAvailabilityStatus.Offline,
            DriverAvailabilityStatus.InRide,
        ],
        [DriverAvailabilityStatus.InRide] =
        [
            DriverAvailabilityStatus.Online,
            DriverAvailabilityStatus.Offline,
        ],
    };

    public Result<DriverAvailabilityHistory> TryUpdateAvailabilityStatus(
        Guid driverId,
        DriverAvailabilityStatus? currentStatus,
        DriverAvailabilityStatus newStatus)
    {
        if (currentStatus is null)
        {
            return DriverAvailabilityHistory.Create(driverId, newStatus);
        }

        if (currentStatus == newStatus)
        {
            return Result<DriverAvailabilityHistory>.FromInvalid(
                Error.Create("Driver.Status", "O motorista já está nesse status."));
        }

        if (!ValidTransitions.TryGetValue(currentStatus!.Value,
                out DriverAvailabilityStatus[]? allowedStatuses)
            || !allowedStatuses.Contains(newStatus))
        {
            return Result<DriverAvailabilityHistory>.FromRuleViolation(
                Error.Create("Driver.Status", $"Transição inválida de {driverId} para {newStatus}."));
        }

        return DriverAvailabilityHistory.Create(driverId, newStatus);
    }
}
