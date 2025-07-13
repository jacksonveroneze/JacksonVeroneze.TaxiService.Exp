using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Driver.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class DriverInactivedEvent(
    Guid aggregateId,
    DateTime inactivedOnUtc)
    : BaseDomainEvent(aggregateId, nameof(DriverInactivedEvent))
{
    public DateTime InactivedOnUtc { get; } = inactivedOnUtc;
}
