using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Passengers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class EmailError
    {
        public static Error NotFound =>
            new("Email.NotFound",
                string.Format(null, CommonDomainErrors.TemplateNotFound, "e-mail"));

        public static Error DuplicateEmail =>
            new("Email.DuplicateEmail",
                string.Format(null, CommonDomainErrors.TemplateDataInUse, "e-mail"));

        public static Error InvalidEmail =>
            new("Email.InvalidEmail",
                string.Format(null, CommonDomainErrors.TemplateDataInvalid, "e-mail"));
    }
}
