using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

public static class DomainErrors
{
    public static class Bank
    {
        public static Error NotFound =>
            new("Bank.NotFound",
                "The bank with the specified identifier was not found.");

        public static Error AlreadyProcessed =>
            new("Bank.AlreadyProcessed",
                "The bank has already been processed.");

        public static Error DuplicateName =>
            new("Bank.DuplicateName",
                "The specified name is already in use.");

        public static Error StatusNotAllowed =>
            new("Bank.ChangeStatus",
                "The status status not allowed");
    }

    public static class Client
    {
    }
}
