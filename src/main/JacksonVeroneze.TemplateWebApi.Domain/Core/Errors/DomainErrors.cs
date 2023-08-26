using JacksonVeroneze.NET.DomainObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static Error NotFound =>
            new("User.NotFound",
                "The user with the specified identifier was not found.");

        public static Error AlreadyActivated =>
            new("User.AlreadyActivated",
                "The user has already been activated.");

        public static Error AlreadyInactivated =>
            new("User.AlreadyInactivated",
                "The user has already been inactivated.");

        public static Error DuplicateName =>
            new("User.DuplicateName",
                "The specified name is already in use.");

        public static Error PhoneNotFound =>
            new("User.PhoneNotFound",
                "The phone with the specified identifier was not found.");

        public static Error DuplicatePhone =>
            new("User.DuplicatePhone",
                "The specified phone is already in use.");

        public static Error EmailNotFound =>
            new("User.EmailNotFound",
                "The email with the specified identifier was not found.");

        public static Error DuplicateEmail =>
            new("User.DuplicateEmail",
                "The specified email is already in use.");
    }
}
