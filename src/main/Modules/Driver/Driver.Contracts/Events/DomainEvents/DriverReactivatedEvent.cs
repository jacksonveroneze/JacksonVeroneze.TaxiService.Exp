using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Driver.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class DriverReactivatedEvent(
    Guid aggregateId,
    DateTime reactivatedOnUtc)
    : BaseDomainEvent(aggregateId, nameof(DriverReactivatedEvent))
{
    public DateTime ReactivatedOnUtc { get; } = reactivatedOnUtc;
}
