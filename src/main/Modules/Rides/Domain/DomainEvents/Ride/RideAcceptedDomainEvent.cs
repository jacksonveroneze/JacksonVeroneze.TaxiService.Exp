using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.Domain.DomainEvents.Base;

namespace TaxiService.Modules.Rides.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideAcceptedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "accepted");