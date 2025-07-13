using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Driver.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class DriverRemovedEvent(
    Guid aggregateId,
    DateTime removedOnUtc)
    : BaseDomainEvent(aggregateId, nameof(DriverRemovedEvent))
{
    public DateTime RemovedOnUtc { get; } = removedOnUtc;
}
