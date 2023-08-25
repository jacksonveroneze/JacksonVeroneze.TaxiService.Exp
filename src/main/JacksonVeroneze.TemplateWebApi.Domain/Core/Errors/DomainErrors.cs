using JacksonVeroneze.NET.DomainObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static Error NotFound =>
            new("User.NotFound",
                "The user with the specified identifier was not found.");

        public static Error AlreadyProcessed =>
            new("User.AlreadyProcessed",
                "The user has already been processed.");

        public static Error DuplicateName =>
            new("User.DuplicateName",
                "The specified name is already in use.");

        public static Error StatusNotAllowed =>
            new("User.ChangeStatus",
                "The status status not allowed");
    }
}
