using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class DriverError
    {
        public static Error NotFound =>
            new("Driver.NotFound",
                string.Format(null, TemplateNotFound, "driver id"));

        public static Error InvalidName =>
            new("Driver.InvalidName",
                string.Format(null, TemplateDataInvalid, "name"));

        public static Error InvalidCpf =>
            new("Driver.InvalidCpf",
                string.Format(null, TemplateDataInvalid, "cpf"));

        public static Error DuplicateEmail =>
            new("Driver.DuplicateEmail",
                string.Format(null, TemplateDataInUse, "e-mail"));

        public static Error AlreadyActivated =>
            new("Driver.AlreadyActivated",
                "The user has already been activated.");

        public static Error AlreadyInactivated =>
            new("Driver.AlreadyInactivated",
                "The user has already been inactivated.");
    }
}