using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Payments.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class SignatureError
    {
        public static Error SigningError =>
            new("Signature.SigningError",
                "Document could not be signed by any provider.");

        public static Error SigningErrorProvider =>
            new("Signature.SigningError",
                "Document could not be signed by any provider.");
    }
}