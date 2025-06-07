using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.PublicApi.v1.Queries;

public sealed record DriverPublicApiFilter
{
    public ICollection<Guid>? Ids { get; init; }

    public DriverStatus? Status { get; init; }
}
