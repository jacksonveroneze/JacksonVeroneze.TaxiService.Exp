namespace TaxiService.Modules.Drivers.PublicApi.Queries;

public sealed record PublicApiDriverFilter(ICollection<Guid?> Ids);
