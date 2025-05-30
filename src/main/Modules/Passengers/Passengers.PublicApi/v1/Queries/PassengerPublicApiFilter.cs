namespace TaxiService.Modules.Passengers.PublicApi.v1.Queries;

public sealed record PassengerPublicApiFilter(
    ICollection<Guid> Ids);
