using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Payments.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class ProviderError
    {
        public static Error Unavailable =>
            Error.Create("Providers.Unavailable",
                "No payments providers are available.");
    }
}
