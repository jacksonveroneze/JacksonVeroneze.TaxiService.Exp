using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Passenger.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class EmailError
    {
        public static Error NotFound =>
            Error.Create("Email.NotFound",
                string.Format(CommonDomainErrors.TemplateNotFound, "e-mail"));
    }
}
