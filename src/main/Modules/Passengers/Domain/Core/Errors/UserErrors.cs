using JacksonVeroneze.NET.Result;
using TaxiService.Common.Domain.Core.Errors;

namespace TaxiService.Modules.Passengers.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class UserError
    {
        public static Error NotFound =>
            new("User.NotFound",
                string.Format(null, CommonDomainErrors.TemplateNotFound, "user id"));

        public static Error InvalidName =>
            new("User.InvalidName",
                string.Format(null, CommonDomainErrors.TemplateDataInvalid, "name"));

        public static Error InvalidCpf =>
            new("User.InvalidCpf",
                string.Format(null, CommonDomainErrors.TemplateDataInvalid, "cpf"));

        public static Error DuplicateEmail =>
            new("User.DuplicateEmail",
                string.Format(null, CommonDomainErrors.TemplateDataInUse, "e-mail"));

        public static Error AlreadyActivated =>
            new("User.AlreadyActivated",
                "The user has already been activated.");

        public static Error AlreadyInactivated =>
            new("User.AlreadyInactivated",
                "The user has already been inactivated.");
    }
}