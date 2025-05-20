using System.Diagnostics.CodeAnalysis;
using System.Text;
using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Errors;

[ExcludeFromCodeCoverage]
internal static class ValidationErrors
{
    private static readonly CompositeFormat TemplateIsRequired =
        CompositeFormat.Parse("The {0} is required.");

    internal static class Passenger
    {
        internal static Error IdIsRequired =>
            Error.Create("Passenger.IdIsRequired",
                string.Format(null, TemplateIsRequired, "id"));

        internal static Error NameIsRequired =>
            Error.Create("Passenger.NameIsRequired",
                string.Format(null, TemplateIsRequired, "name"));

        internal static Error BirthdayIsRequired =>
            Error.Create("Passenger.BirthdayIsRequired",
                string.Format(null, TemplateIsRequired, "birthday"));

        internal static Error GenderIsRequired =>
            Error.Create("Passenger.GenderIsRequired",
                string.Format(null, TemplateIsRequired, "gender"));

        internal static Error EmailIsRequired =>
            Error.Create("Passenger.EmailIsRequired",
                string.Format(null, TemplateIsRequired, "e-mail"));

        internal static Error DocumentIsRequired =>
            Error.Create("Passenger.DocumentIsRequired",
                string.Format(null, TemplateIsRequired, "document"));

        internal static Error EmailIsInvalid =>
            Error.Create("Passenger.EmailIsInvalid",
                "The e-mail is invalid.");

        internal static Error DocumentIsInvalid =>
            Error.Create("Passenger.DocumentIsInvalid",
                "The document is invalid.");
    }
}
