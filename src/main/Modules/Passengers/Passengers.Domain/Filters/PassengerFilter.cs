using System.Diagnostics.CodeAnalysis;

namespace TaxiService.Modules.Passengers.Domain.Filters;

[ExcludeFromCodeCoverage]
public record PassengerFilter
{
    public ICollection<Guid>? Ids { get; init; }
}
