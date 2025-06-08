using System.Diagnostics.CodeAnalysis;

namespace TaxiService.Modules.Passenger.Application.v1.Passenger.Common.Filters;

[ExcludeFromCodeCoverage]
public record PassengerFilter
{
    public ICollection<Guid>? Ids { get; init; }
}
