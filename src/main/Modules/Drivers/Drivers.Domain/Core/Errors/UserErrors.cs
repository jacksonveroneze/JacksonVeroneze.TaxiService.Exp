using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Drivers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class DriverError
    {
        public static Error NotFound =>
            new("Driver.NotFound",
                string.Format(null, CommonDomainErrors.TemplateNotFound, "driver id"));

        public static Error InvalidName =>
            new("Driver.InvalidName",
                string.Format(null, CommonDomainErrors.TemplateDataInvalid, "name"));

        public static Error InvalidCpf =>
            new("Driver.InvalidCpf",
                string.Format(null, CommonDomainErrors.TemplateDataInvalid, "cpf"));

        public static Error DuplicateEmail =>
            new("Driver.DuplicateEmail",
                string.Format(null, CommonDomainErrors.TemplateDataInUse, "e-mail"));

        public static Error AlreadyActivated =>
            new("Driver.AlreadyActivated",
                "The Driver has already been activated.");

        public static Error AlreadyInactivated =>
            new("Driver.AlreadyInactivated",
                "The Driver has already been inactivated.");
    }
}