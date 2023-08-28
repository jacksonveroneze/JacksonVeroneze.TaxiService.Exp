using JacksonVeroneze.NET.DomainObjects.Errors;

namespace JacksonVeroneze.TemplateWebApi.Application.Core.Errors;

internal static class ValidationErrors
{
    internal static class User
    {
        internal static Error IdIsRequired =>
            new("User.IdIsRequired",
                "The ID is required.");

        internal static Error NameIsRequired =>
            new("User.NameIsRequired",
                "The name is required.");

        internal static Error BirthdayIsRequired =>
            new("User.BirthdayIsRequired",
                "The birthday is required.");

        internal static Error GenderIsRequired =>
            new("User.GenderIsRequired",
                "The gender is required.");

        internal static Error EmailIsRequired =>
            new("User.EmailIsRequired",
                "The e-mail is required.");
    }
}
