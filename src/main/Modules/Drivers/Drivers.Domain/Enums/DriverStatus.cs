namespace TaxiService.Modules.Drivers.Domain.Enums;

public enum DriverStatus
{
    None,
    PendingActivation,
    Active,
    Inactive,
    BlockedTemporarily,
    BlockedPermanent,
}