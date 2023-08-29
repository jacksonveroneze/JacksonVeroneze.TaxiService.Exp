using JacksonVeroneze.NET.DomainObjects.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

public static class DomainErrors
{
    private static string _templateNotFound = "The {0} with the specified identifier was not found.";

    public static class User
    {
        public static Error NotFound =>
            new("User.NotFound",
                "The user with the specified identifier was not found.");

        public static Error DuplicateName =>
            new("User.DuplicateName",
                "The specified name is already in use.");

        public static Error InvalidName =>
            new("User.InvalidName",
                "The specified name is invalid.");

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
                "The email with the specified identifier was not found.");

        public static Error DuplicateEmail =>
            new("User.DuplicateEmail",
                "The specified email is already in use.");

        public static Error InvalidEmail =>
            new("User.InvalidEmail",
                "The specified email is invalid.");
    }

    public static class Phone
    {
        public static Error NotFound =>
            new("User.NotFound",
                "The phone with the specified identifier was not found.");

        public static Error DuplicatePhone =>
            new("User.DuplicatePhone",
                "The specified phone is already in use.");

        public static Error InvalidPhone =>
            new("User.InvalidPhone",
                "The specified phone is invalid.");
    }
}
