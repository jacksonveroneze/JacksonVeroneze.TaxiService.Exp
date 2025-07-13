using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Ride.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class RideAcceptedDomainEvent(
    Guid aggregateId,
    Guid passengerId,
    Guid driverId,
    DateTime acceptedAtUtc)
    : BaseDomainEvent(aggregateId, nameof(RideAcceptedDomainEvent))
{
    public Guid DriverId { get; } = driverId;
    public Guid PassengerId { get; } = passengerId;
    public DateTime AcceptedAtUtc { get; } = acceptedAtUtc;
}
