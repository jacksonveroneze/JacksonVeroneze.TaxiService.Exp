namespace TaxiService.Modules.Passenger.PublicApi.v1.Queries;

public sealed record PassengerPublicApiFilter(
    ICollection<Guid> Ids);
