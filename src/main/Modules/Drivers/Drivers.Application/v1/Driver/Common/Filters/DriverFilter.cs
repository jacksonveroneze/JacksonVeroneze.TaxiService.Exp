using System.Diagnostics.CodeAnalysis;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Common.Filters;

[ExcludeFromCodeCoverage]
public record DriverFilter
{
    public ICollection<Guid>? Ids { get; init; }
}
