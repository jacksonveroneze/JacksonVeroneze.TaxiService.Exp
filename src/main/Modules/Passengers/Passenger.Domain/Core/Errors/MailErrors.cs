using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Passenger.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class EmailError
    {
        public static Error NotFound =>
            Error.Create("Email.NotFound",
                string.Format(null, CommonDomainErrors.TemplateNotFound, "e-mail"));

        public static Error DuplicateEmail =>
            Error.Create("Email.DuplicateEmail",
                string.Format(null, CommonDomainErrors.TemplateDataInUse, "e-mail"));

        public static Error InvalidEmail =>
            Error.Create("Email.InvalidEmail",
                string.Format(null, CommonDomainErrors.TemplateDataInvalid, "e-mail"));
    }
}
