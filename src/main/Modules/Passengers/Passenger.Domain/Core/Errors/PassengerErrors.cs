using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;

namespace TaxiService.Modules.Passenger.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class PassengerError
    {
        public static Error NotFound =>
            Error.Create("Passenger.NotFound",
                string.Format(CommonDomainErrors.TemplateNotFound, "passenger id"));

        public static Error DuplicateEmail =>
            Error.Create("Passenger.DuplicateEmail",
                string.Format(CommonDomainErrors.TemplateDataInUse, "e-mail"));

        public static Error AlreadyActivated =>
            Error.Create("Passenger.AlreadyActivated",
                "The passenger has already been activated.");

        public static Error AlreadyInactivated =>
            Error.Create("Passenger.AlreadyInactivated",
                "The passenger has already been inactivated.");
    }
}
