using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseDomain.Core.Errors;

public static partial class CommonDomainErrors
{
    internal static partial class EmailError
    {
        public static Error InvalidEmail =>
            Error.Create("Email.InvalidEmail",
                string.Format(TemplateDataInvalid, "e-mail"));
    }
}
