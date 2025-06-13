using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Driver.Domain.Events;

[ExcludeFromCodeCoverage]
public class DriverInactivedEvent(
    Guid aggregateId,
    DateTime inactivedOnUtc)
    : BaseDomainEvent(aggregateId, nameof(DriverInactivedEvent))
{
    public DateTime InactivedOnUtc { get; } = inactivedOnUtc;
}
