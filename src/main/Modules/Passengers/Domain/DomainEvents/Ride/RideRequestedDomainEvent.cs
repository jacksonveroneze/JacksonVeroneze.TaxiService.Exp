using System.Diagnostics.CodeAnalysis;
using TaxiService.Modules.Passengers.Domain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideRequestedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "requested");