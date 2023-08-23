using JacksonVeroneze.NET.DomainObjects;

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
        public static Error NotFound =>
            new("Client.NotFound",
                "The client with the specified identifier was not found.");

        public static Error DuplicateDocument =>
            new("Bank.Document",
                "The specified document is already in use.");
    }
}
