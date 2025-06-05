using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Driver.Domain.Core.Errors;

static partial class DomainErrors
{
    public static class DriverErrors
    {
        public static Error NotFound =>
            Error.Create("Driver.NotFound",
                string.Format(CommonDomainErrors.TemplateNotFound, "driver"));

        public static Error InvalidName =>
            Error.Create("Driver.InvalidName",
                string.Format(CommonDomainErrors.TemplateDataInvalid, "name"));

        public static Error InvalidCpf =>
            Error.Create("Driver.InvalidCpf",
                string.Format(CommonDomainErrors.TemplateDataInvalid, "cpf"));

        public static Error DuplicateEmail =>
            Error.Create("Driver.DuplicateEmail",
                string.Format(CommonDomainErrors.TemplateDataInUse, "e-mail"));

        public static Error DuplicateEmailOrDocument =>
            Error.Create("Driver.DuplicateEmailOrDocument",
                string.Format(CommonDomainErrors.TemplateDataInUse, "e-mail or document"));

        public static Error NotPendingApproval =>
            Error.Create("Driver.NotPendingApproval", "Only pending Drivers can be approved.");

        public static Error CannotBlockPermanently =>
            Error.Create("Driver.CannotBlockPermanently", "The Driver is already permanently blocked.");

        public static Error AlreadyBlockedTemporarily =>
            Error.Create("Driver.AlreadyBlockedTemporarily", "The Driver is already temporarily blocked.");

        public static Error AlreadyInactivated =>
            Error.Create("Driver.AlreadyInactivated", "The Driver has already been inactivated.");

        public static Error AlreadyActive =>
            Error.Create("Driver.AlreadyActive", "The Driver is already active.");

        public static Error NotActivated =>
            Error.Create("Driver.NotActivated", "The Driver is not activated.");

        public static Error DriverHasActiveRide =>
            Error.Create("Driver.HasActiveRide",
                "The driver cannot perform this action because he has an active race.");
    }
}
