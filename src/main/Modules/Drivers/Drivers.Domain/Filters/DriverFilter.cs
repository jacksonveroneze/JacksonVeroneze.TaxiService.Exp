using System.Diagnostics.CodeAnalysis;

namespace TaxiService.Modules.Drivers.Domain.Filters;

[ExcludeFromCodeCoverage]
public record DriverFilter
{
    public ICollection<Guid?>? Ids { get; init; }
}
