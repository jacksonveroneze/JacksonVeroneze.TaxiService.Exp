using System.Diagnostics.CodeAnalysis;
using System.Text;
using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.Domain.Core.Errors;

[ExcludeFromCodeCoverage]
public static partial class CommonDomainErrors
{
    public static readonly CompositeFormat TemplateNotFound =
        CompositeFormat.Parse("The {0} with the specified identifier was not found.");

    public static readonly CompositeFormat TemplateDataInUse =
        CompositeFormat.Parse("The specified {0} is already in use.");

    public static readonly CompositeFormat TemplateDataInvalid =
        CompositeFormat.Parse("The specified {0} is invalid.");

    public static class CommonError
    {
        public static Error NotFound =>
            new("Commmon.NotFound",
                string.Format(null, TemplateNotFound, "resource"));
    }

    public static class CoordinateError
    {
        public static Error InvalidCoordinate =>
            new("Coordinate.InvalidCoordinate",
                string.Format(null, TemplateDataInvalid, "coordinate"));
    }

    public static class MoneyError
    {
        public static Error InvalidValue =>
            new("Money.InvalidValue", "Valor inválido");
    }
}