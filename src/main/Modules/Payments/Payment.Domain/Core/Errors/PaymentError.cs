using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Payment.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class PaymentError
    {
        public static Error PayError =>
            Error.Create("Payment.PayError",
                "Payment could not be paid by any provider.");

        public static Error PayErrorProvider =>
            Error.Create("Payment.PayErrorProvider",
                "Payment could not be paid by any provider.");
    }
}
