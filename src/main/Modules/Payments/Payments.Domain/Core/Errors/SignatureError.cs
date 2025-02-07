using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Payments.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class PaymentError
    {
        public static Error SigningError =>
            new("Payment.SigningError",
                "Payment could not be paid by any provider.");

        public static Error SigningErrorProvider =>
            new("Payment.SigningError",
                "Payment could not be paid by any provider.");
    }
}