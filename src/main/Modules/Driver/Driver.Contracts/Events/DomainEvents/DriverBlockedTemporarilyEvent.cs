using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Driver.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class DriverBlockedTemporarilyEvent(
    Guid aggregateId,
    DateTime blockedOn)
    : BaseDomainEvent(aggregateId, nameof(DriverBlockedTemporarilyEvent))
{
    public DateTime BlockedOn { get; } = blockedOn;
}
