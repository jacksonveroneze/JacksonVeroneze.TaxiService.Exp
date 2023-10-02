using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TemplateWebApi.Application.Core.Errors;

[ExcludeFromCodeCoverage]
internal static class ValidationErrors
{
    private const string TemplateIsRequired = "The {0} is required.";

    internal static class User
    {
        internal static Error IdIsRequired =>
            new("User.IdIsRequired",
                string.Format(TemplateIsRequired, "id"));

        internal static Error NameIsRequired =>
            new("User.NameIsRequired",
                string.Format(TemplateIsRequired, "name"));

        internal static Error BirthdayIsRequired =>
            new("User.BirthdayIsRequired",
                string.Format(TemplateIsRequired, "birthday"));

        internal static Error GenderIsRequired =>
            new("User.GenderIsRequired",
                string.Format(TemplateIsRequired, "gender"));

        internal static Error EmailIsRequired =>
            new("User.EmailIsRequired",
                string.Format(TemplateIsRequired, "e-mail"));

        internal static Error DocumentIsRequired =>
            new("User.DocumentIsRequired",
                string.Format(TemplateIsRequired, "document"));

        internal static Error DocumentIsInvalid =>
            new("User.DocumentIsInvalid",
                "The document is invalid.");
    }
}
