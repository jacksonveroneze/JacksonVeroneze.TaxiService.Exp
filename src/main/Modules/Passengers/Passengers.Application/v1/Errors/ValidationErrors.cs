using System.Diagnostics.CodeAnalysis;
using System.Text;
using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Errors;

[ExcludeFromCodeCoverage]
internal static class ValidationErrors
{
    private static readonly CompositeFormat TemplateIsRequired =
        CompositeFormat.Parse("The {0} is required.");

    internal static class Passanger
    {
        internal static Error IdIsRequired =>
            new("Passanger.IdIsRequired",
                string.Format(null, TemplateIsRequired, "id"));

        internal static Error NameIsRequired =>
            new("Passanger.NameIsRequired",
                string.Format(null, TemplateIsRequired, "name"));

        internal static Error BirthdayIsRequired =>
            new("Passanger.BirthdayIsRequired",
                string.Format(null, TemplateIsRequired, "birthday"));

        internal static Error GenderIsRequired =>
            new("Passanger.GenderIsRequired",
                string.Format(null, TemplateIsRequired, "gender"));

        internal static Error EmailIsRequired =>
            new("Passanger.EmailIsRequired",
                string.Format(null, TemplateIsRequired, "e-mail"));

        internal static Error DocumentIsRequired =>
            new("Passanger.DocumentIsRequired",
                string.Format(null, TemplateIsRequired, "document"));

        internal static Error EmailIsInvalid =>
            new("Passanger.EmailIsInvalid",
                "The e-mail is invalid.");

        internal static Error DocumentIsInvalid =>
            new("Passanger.DocumentIsInvalid",
                "The document is invalid.");
    }
}