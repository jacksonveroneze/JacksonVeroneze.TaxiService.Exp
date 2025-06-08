namespace TaxiService.Modules.Payment.Domain.Enums;

public enum PaymentStatus
{
    None,
    Requested,
    Accepted,
    InProgress,
    Completed,
    Canceled
}
