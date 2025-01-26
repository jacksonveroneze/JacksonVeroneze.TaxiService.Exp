using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class EmailError
    {
        public static Error NotFound =>
            new("Email.NotFound",
                string.Format(null, TemplateNotFound, "e-mail"));

        public static Error DuplicateEmail =>
            new("Email.DuplicateEmail",
                string.Format(null, TemplateDataInUse, "e-mail"));

        public static Error InvalidEmail =>
            new("Email.InvalidEmail",
                string.Format(null, TemplateDataInvalid, "e-mail"));
    }
}