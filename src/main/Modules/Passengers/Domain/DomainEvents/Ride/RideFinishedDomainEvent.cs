using System.Diagnostics.CodeAnalysis;
using TaxiService.Modules.Passengers.Domain.DomainEvents.Base;

namespace TaxiService.Modules.Passengers.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideFinishedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "finished");