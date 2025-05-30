namespace TaxiService.Modules.Drivers.PublicApi.v1.Queries;

public sealed record DriverPublicApiFilter(
    ICollection<Guid> Ids);
