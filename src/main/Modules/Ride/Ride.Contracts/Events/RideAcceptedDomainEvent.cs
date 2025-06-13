using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Ride.Contracts.Events;

[ExcludeFromCodeCoverage]
public class RideAcceptedDomainEvent(
    Guid aggregateId,
    Guid driverId,
    DateTime acceptedAtUtc)
    : BaseDomainEvent(aggregateId, nameof(RideAcceptedDomainEvent))
{
    public Guid DriverId { get; } = driverId;
    public DateTime AcceptedAtUtc { get; } = acceptedAtUtc;
}
