namespace TaxiService.Modules.Passengers.PublicApi.Queries;

public sealed record PublicApiPassengerFilter(ICollection<Guid?> Ids);
