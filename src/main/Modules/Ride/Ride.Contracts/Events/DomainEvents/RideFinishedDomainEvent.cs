using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Ride.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class RideFinishedDomainEvent(
    Guid aggregateId,
    Guid driverId,
    Guid passengerId,
    DateTime finishedAtUtc,
    TimeSpan duration,
    double distanceKm,
    decimal price)
    : BaseDomainEvent(aggregateId, nameof(RideFinishedDomainEvent))
{
    public Guid DriverId { get; } = driverId;
    public Guid PassengerId { get; } = passengerId;
    public DateTime FinishedAtUtc { get; } = finishedAtUtc;
    public TimeSpan Duration { get; } = duration;
    public double DistanceKm { get; } = distanceKm;
    public decimal Price { get; } = price;
}
