using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.BaseDomain.DomainEvents.Base;

namespace TaxiService.Modules.Ride.Contracts.Events;

[ExcludeFromCodeCoverage]
public class RideCanceledDomainEvent(
    Guid aggregateId,
    Guid? driverId,
    DateTime canceledAtUtc)
    : BaseDomainEvent(aggregateId, nameof(RideCanceledDomainEvent))
{
    public Guid? DriverId { get; } = driverId;
    public DateTime CanceledAtUtc { get; } = canceledAtUtc;
}
