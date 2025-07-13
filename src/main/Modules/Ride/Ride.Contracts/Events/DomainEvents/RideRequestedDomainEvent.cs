using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Ride.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class RideRequestedDomainEvent(
    Guid aggregateId,
    Guid passengerId,
    double latitudeFrom,
    double longitudeFrom,
    double latitudeTo,
    double longitudeTo,
    DateTime requestedAtUtc)
    : BaseDomainEvent(aggregateId, nameof(RideRequestedDomainEvent))
{
    public Guid PassengerId { get; } = passengerId;

    public double LatitudeFrom { get; } = latitudeFrom;

    public double LongitudeFrom { get; } = longitudeFrom;

    public double LatitudeTo { get; } = latitudeTo;

    public double LongitudeTo { get; } = longitudeTo;

    public DateTime RequestedAtUtc { get; } = requestedAtUtc;
}
