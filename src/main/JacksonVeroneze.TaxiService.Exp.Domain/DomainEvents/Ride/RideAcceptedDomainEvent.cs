using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideAcceptedDomainEvent : BaseDomainEvent
{
    public RideAcceptedDomainEvent(Guid aggregateId) :
        base(aggregateId, "accepted")
    {
    }
}
