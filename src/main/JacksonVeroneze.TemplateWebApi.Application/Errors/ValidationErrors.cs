using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Application.Errors;

public class ValidationErrors
{
    internal static class Bank
    {
        internal static Error NameIsRequired =>
            new("Bank.NameIsRequired", "The name is required.");
    }
}
