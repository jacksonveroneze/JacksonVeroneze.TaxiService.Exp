using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Payment.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class PaymentError
    {
        public static Error SigningError =>
            Error.Create("Payment.SigningError",
                "Payment could not be paid by any provider.");

        public static Error SigningErrorProvider =>
            Error.Create("Payment.SigningError",
                "Payment could not be paid by any provider.");
    }
}
