using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Driver.Domain.Events;

[ExcludeFromCodeCoverage]
public class DriverActivatedEvent(
    Guid aggregateId,
    DateTime activatedOnUtc)
    : BaseDomainEvent(aggregateId, nameof(DriverActivatedEvent))
{
    public DateTime ActivatedOnUtc { get; } = activatedOnUtc;
}
