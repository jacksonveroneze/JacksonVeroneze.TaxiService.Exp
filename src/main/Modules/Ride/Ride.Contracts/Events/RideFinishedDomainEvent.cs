using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Ride.Contracts.Events;

[ExcludeFromCodeCoverage]
public class RideFinishedDomainEvent(
    Guid aggregateId,
    Guid driverId,
    DateTime finishedAtUtc,
    TimeSpan duration,
    double distanceKm,
    decimal price)
    : BaseDomainEvent(aggregateId, nameof(RideFinishedDomainEvent))
{
    public Guid DriverId { get; } = driverId;
    public DateTime FinishedAtUtc { get; } = finishedAtUtc;
    public TimeSpan Duration { get; } = duration;
    public double DistanceKm { get; } = distanceKm;
    public decimal Price { get; } = price;
}
