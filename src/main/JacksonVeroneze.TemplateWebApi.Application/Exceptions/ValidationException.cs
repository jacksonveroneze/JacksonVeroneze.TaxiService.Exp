using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.TemplateWebApi.Application.Exceptions;

[ExcludeFromCodeCoverage]
public class ValidationException(
    IDictionary<string, string[]> errorsDictionary)
    : ApplicationException("Validation Failure")
{
    public IDictionary<string, string[]> ErrorsDictionary { get; } = errorsDictionary;
}
