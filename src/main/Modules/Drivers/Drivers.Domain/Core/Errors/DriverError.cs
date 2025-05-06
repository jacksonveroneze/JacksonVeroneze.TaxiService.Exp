using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Drivers.Domain.Core.Errors;

static partial class DomainErrors
{
    public static class DriverErrors
    {
        public static Error NotFound =>
            new("Driver.NotFound",
                string.Format(CommonDomainErrors.TemplateNotFound, "driver"));

        public static Error InvalidName =>
            new("Driver.InvalidName",
                string.Format(CommonDomainErrors.TemplateDataInvalid, "name"));

        public static Error InvalidCpf =>
            new("Driver.InvalidCpf",
                string.Format(CommonDomainErrors.TemplateDataInvalid, "cpf"));

        public static Error DuplicateEmail =>
            new("Driver.DuplicateEmail",
                string.Format(CommonDomainErrors.TemplateDataInUse, "e-mail"));

        public static Error DuplicateEmailOrDocument =>
            new("Driver.DuplicateEmailOrDocument",
                string.Format(CommonDomainErrors.TemplateDataInUse, "e-mail or document"));

        public static Error NotPendingApproval =>
            new("Driver.NotPendingApproval", "Only pending Drivers can be approved.");

        public static Error CannotBlockPermanently =>
            new("Driver.CannotBlockPermanently", "The Driver is already permanently blocked.");

        public static Error AlreadyBlockedTemporarily =>
            new("Driver.AlreadyBlockedTemporarily", "The Driver is already temporarily blocked.");

        public static Error AlreadyInactivated =>
            new("Driver.AlreadyInactivated", "The Driver has already been inactivated.");

        public static Error AlreadyActive =>
            new("Driver.AlreadyActive", "The Driver is already active.");

        public static Error NotActivated =>
            new("Driver.NotActivated", "The Driver is not activated.");

        public static Error DriverHasActiveRide =>
            new("Driver.HasActiveRide", "The driver cannot perform this action because he has an active race.");
    }
}
