namespace TaxiService.Modules.Driver.PublicApi.v1.Queries;

public sealed record VehiclePublicApiFilter
{
    public ICollection<Guid>? Ids { get; init; }

    public ICollection<Guid>? DriversIds { get; init; }
}
