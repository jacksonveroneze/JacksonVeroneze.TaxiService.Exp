using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideRequestedDomainEvent : BaseDomainEvent
{
    public RideRequestedDomainEvent(Guid aggregateId) :
        base(aggregateId, "requested")
    {
    }
}
