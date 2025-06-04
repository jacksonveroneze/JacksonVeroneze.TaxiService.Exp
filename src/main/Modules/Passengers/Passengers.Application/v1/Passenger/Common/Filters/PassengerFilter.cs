using System.Diagnostics.CodeAnalysis;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Filters;

[ExcludeFromCodeCoverage]
public record PassengerFilter
{
    public ICollection<Guid>? Ids { get; init; }
}
