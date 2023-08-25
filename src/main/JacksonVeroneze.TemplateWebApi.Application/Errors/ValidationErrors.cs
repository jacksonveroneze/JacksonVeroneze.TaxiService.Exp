using JacksonVeroneze.NET.DomainObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.Errors;

public class ValidationErrors
{
    internal static class User
    {
        internal static Error NameIsRequired =>
            new("User.NameIsRequired", "The name is required.");
    }
}
