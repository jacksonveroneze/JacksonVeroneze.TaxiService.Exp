using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Ride.Contracts.Events;

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
