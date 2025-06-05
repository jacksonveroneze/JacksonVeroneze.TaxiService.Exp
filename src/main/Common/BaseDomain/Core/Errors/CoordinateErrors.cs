using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseDomain.Core.Errors;

public static partial class CommonDomainErrors
{
    public static partial class CoordinateErrors
    {
        public static Error InvalidCoordinate =>
            Error.Create("Coordinate.InvalidCoordinate",
                string.Format(TemplateDataInvalid, "coordinate"));
    }
}
