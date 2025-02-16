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

        #region Status

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

        #endregion
    }
}