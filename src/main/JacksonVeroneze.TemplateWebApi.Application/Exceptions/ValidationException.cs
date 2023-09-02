using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.TemplateWebApi.Application.Exceptions;

[ExcludeFromCodeCoverage]
public class ValidationException : ApplicationException
{
    public ValidationException(IDictionary<string, string[]> errorsDictionary)
        : base("Validation Failure")
        => ErrorsDictionary = errorsDictionary;

    public IDictionary<string, string[]> ErrorsDictionary { get; }
}
