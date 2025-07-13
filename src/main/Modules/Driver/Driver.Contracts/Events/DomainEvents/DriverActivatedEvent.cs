using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Driver.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class DriverActivatedEvent(
    Guid aggregateId,
    DateTime activatedOnUtc)
    : BaseDomainEvent(aggregateId, nameof(DriverActivatedEvent))
{
    public DateTime ActivatedOnUtc { get; } = activatedOnUtc;
}
