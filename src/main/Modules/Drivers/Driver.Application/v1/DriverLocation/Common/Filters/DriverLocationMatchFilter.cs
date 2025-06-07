using System.Diagnostics.CodeAnalysis;

namespace TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Filters;

[ExcludeFromCodeCoverage]
public record DriverLocationMatchFilter
{
    public double Latitude { get; init; }

    public double Longitude { get; init; }

    public double DistanceInMeters { get; init; }

    public int MaxResults { get; init; }
}
