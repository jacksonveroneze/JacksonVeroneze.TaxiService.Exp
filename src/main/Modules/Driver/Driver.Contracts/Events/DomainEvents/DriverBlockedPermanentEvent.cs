using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Driver.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class DriverBlockedPermanentEvent(
    Guid aggregateId, 
    DateTime blockedOn)
    : BaseDomainEvent(aggregateId, nameof(DriverBlockedPermanentEvent))
{
    public DateTime BlockedOn { get; } = blockedOn;
}
