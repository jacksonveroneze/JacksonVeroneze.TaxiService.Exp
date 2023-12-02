using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.TaxiService.Exp.Application.Exceptions;

[ExcludeFromCodeCoverage]
public class ValidationException(
    IDictionary<string, string[]> errorsDictionary)
    : ApplicationException("Validation Failure")
{
    public IDictionary<string, string[]> ErrorsDictionary { get; } = errorsDictionary;
}
