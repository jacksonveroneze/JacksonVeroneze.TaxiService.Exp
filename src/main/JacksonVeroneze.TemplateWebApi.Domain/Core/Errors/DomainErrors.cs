using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

public partial class DomainErrors
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
            new("Bank.DuplicateEmail",
                "The specified name is already in use.");
    }

    public static class Client
    {
        public static Error NotFound =>
            new("Client.NotFound",
                "The client with the specified identifier was not found.");

        public static Error AlreadyProcessed =>
            new("Client.AlreadyProcessed",
                "The client has already been processed.");
    }
}
