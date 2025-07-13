using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.Events;

namespace TaxiService.Modules.Ride.Contracts.Events.DomainEvents;

[ExcludeFromCodeCoverage]
public class RideCanceledDomainEvent(
    Guid aggregateId,
    Guid? driverId,
    Guid? passengerId,
    DateTime canceledAtUtc)
    : BaseDomainEvent(aggregateId, nameof(RideCanceledDomainEvent))
{
    public Guid? DriverId { get; } = driverId;

    public Guid? PassengerId { get; } = passengerId;

    public DateTime CanceledAtUtc { get; } = canceledAtUtc;
}
