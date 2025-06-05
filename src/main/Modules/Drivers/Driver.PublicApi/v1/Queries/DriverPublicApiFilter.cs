namespace TaxiService.Modules.Driver.PublicApi.v1.Queries;

public sealed record DriverPublicApiFilter(
    ICollection<Guid> Ids);
