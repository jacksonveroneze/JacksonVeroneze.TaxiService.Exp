using System.Diagnostics.CodeAnalysis;

namespace TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Filters;

[ExcludeFromCodeCoverage]
public record VehicleFilter
{
    public ICollection<Guid>? Ids { get; init; }

    public ICollection<Guid>? DriverIds { get; init; }
}
