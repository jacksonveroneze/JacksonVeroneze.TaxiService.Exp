using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseDomain.Errors;

public static partial class CommonDomainErrors
{
    internal static partial class PhoneError
    {
        public static Error InvalidPhone =>
            Error.Create("Phone.InvalidPhone",
                string.Format(TemplateDataInvalid, "phone"));
    }
}
