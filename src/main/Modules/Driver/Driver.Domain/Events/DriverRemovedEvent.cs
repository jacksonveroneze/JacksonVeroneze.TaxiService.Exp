using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Driver.Domain.Events;

[ExcludeFromCodeCoverage]
public class DriverRemovedEvent(
    Guid aggregateId,
    DateTime removedOnUtc)
    : BaseDomainEvent(aggregateId, nameof(DriverRemovedEvent))
{
    public DateTime RemovedOnUtc { get; } = removedOnUtc;
}
