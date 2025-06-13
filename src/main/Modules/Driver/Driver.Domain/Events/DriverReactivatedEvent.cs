using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Driver.Domain.Events;

[ExcludeFromCodeCoverage]
public class DriverReactivatedEvent(
    Guid aggregateId,
    DateTime reactivatedOnUtc)
    : BaseDomainEvent(aggregateId, nameof(DriverReactivatedEvent))
{
    public DateTime ReactivatedOnUtc { get; } = reactivatedOnUtc;
}
