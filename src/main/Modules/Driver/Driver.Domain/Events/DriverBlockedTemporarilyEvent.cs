using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Driver.Domain.Events;

[ExcludeFromCodeCoverage]
public class DriverBlockedTemporarilyEvent(
    Guid aggregateId,
    DateTime blockedOn)
    : BaseDomainEvent(aggregateId, nameof(DriverBlockedTemporarilyEvent))
{
    public DateTime BlockedOn { get; } = blockedOn;
}
