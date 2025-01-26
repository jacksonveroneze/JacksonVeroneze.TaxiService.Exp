using System.Diagnostics.CodeAnalysis;
using TaxiService.Modules.Rides.Domain.DomainEvents.Base;

namespace TaxiService.Modules.Rides.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideStartedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "started");