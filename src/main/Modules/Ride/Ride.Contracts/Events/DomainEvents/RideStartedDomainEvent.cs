using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Ride.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class RideStartedDomainEvent(
    Guid aggregateId,
    Guid driverId,
    Guid passengerId,
    DateTime startedAtUtc)
    : BaseDomainEvent(aggregateId, nameof(RideStartedDomainEvent))
{
    public Guid DriverId { get; } = driverId;
    public Guid PassengerId { get; } = passengerId;
    public DateTime StartedAtUtc { get; } = startedAtUtc;
}
