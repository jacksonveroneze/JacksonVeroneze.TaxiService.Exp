using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Driver.Domain.Events;

[ExcludeFromCodeCoverage]
public class DriverBlockedPermanentEvent(
    Guid aggregateId, 
    DateTime blockedOn)
    : BaseDomainEvent(aggregateId, nameof(DriverBlockedPermanentEvent))
{
    public DateTime BlockedOn { get; } = blockedOn;
}
