using System.Diagnostics.CodeAnalysis;
using TaxiService.Common.Domain.DomainEvents.Base;

namespace TaxiService.Modules.Rides.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideStartedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "started");