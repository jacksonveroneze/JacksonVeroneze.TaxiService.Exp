using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Passengers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class PassangerError
    {
        public static Error NotFound =>
            Error.Create("Passanger.NotFound",
                string.Format(null, CommonDomainErrors.TemplateNotFound, "passanger id"));

        public static Error InvalidName =>
            Error.Create("Passanger.InvalidName",
                string.Format(null, CommonDomainErrors.TemplateDataInvalid, "name"));

        public static Error InvalidCpf =>
            Error.Create("Passanger.InvalidCpf",
                string.Format(null, CommonDomainErrors.TemplateDataInvalid, "cpf"));

        public static Error DuplicateEmail =>
            Error.Create("Passanger.DuplicateEmail",
                string.Format(null, CommonDomainErrors.TemplateDataInUse, "e-mail"));

        public static Error AlreadyActivated =>
            Error.Create("Passanger.AlreadyActivated",
                "The passanger has already been activated.");

        public static Error AlreadyInactivated =>
            Error.Create("Passanger.AlreadyInactivated",
                "The passanger has already been inactivated.");
    }
}
