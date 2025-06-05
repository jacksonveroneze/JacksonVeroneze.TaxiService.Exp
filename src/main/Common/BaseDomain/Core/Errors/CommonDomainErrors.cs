using System.Diagnostics.CodeAnalysis;

namespace TaxiService.Common.BaseDomain.Core.Errors;

[ExcludeFromCodeCoverage]
public static partial class CommonDomainErrors
{
    public const string TemplateNotFound = "The {0} with the specified identifier was not found.";
    public const string TemplateDataInUse = "The specified {0} is already in use.";
    public const string TemplateDataInvalid = "The specified {0} is invalid.";
}
