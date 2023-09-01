using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.DomainObjects.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

[ExcludeFromCodeCoverage]
public static class DomainErrors
{
    private const string TemplateNotFound = "The {0} with the specified identifier was not found.";
    private const string TemplateDataInUse = "The specified {0} is already in use.";
    private const string TemplateDataInvalid = "The specified {0} is invalid.";

    public static class User
    {
        public static Error NotFound =>
            new("User.NotFound",
                string.Format(TemplateNotFound, "user"));

        public static Error DuplicateName =>
            new("User.DuplicateName",
                string.Format(TemplateDataInUse, "name"));

        public static Error InvalidName =>
            new("User.InvalidName",
                string.Format(TemplateDataInvalid, "name"));

        public static Error InvalidCpf =>
            new("User.InvalidCpf",
                "The document is invalid.");

        public static Error AlreadyActivated =>
            new("User.AlreadyActivated",
                "The user has already been activated.");

        public static Error AlreadyInactivated =>
            new("User.AlreadyInactivated",
                "The user has already been inactivated.");
    }

    public static class Email
    {
        public static Error NotFound =>
            new("User.NotFound",
                string.Format(TemplateNotFound, "e-mail"));

        public static Error DuplicateEmail =>
            new("User.DuplicateEmail",
                string.Format(TemplateDataInUse, "e-mail"));

        public static Error InvalidEmail =>
            new("User.InvalidEmail",
                string.Format(TemplateDataInvalid, "e-mail"));
    }

    public static class Phone
    {
        public static Error NotFound =>
            new("User.NotFound",
                string.Format(TemplateNotFound, "phone"));

        public static Error DuplicatePhone =>
            new("User.DuplicatePhone",
                string.Format(TemplateDataInUse, "phone"));

        public static Error InvalidPhone =>
            new("User.InvalidPhone",
                string.Format(TemplateDataInvalid, "phone"));
    }
}
