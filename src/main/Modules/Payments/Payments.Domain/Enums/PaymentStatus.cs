namespace TaxiService.Modules.Payments.Domain.Enums;

public enum PaymentStatus
{
    None,
    Requested,
    Accepted,
    InProgress,
    Completed,
    Canceled
}