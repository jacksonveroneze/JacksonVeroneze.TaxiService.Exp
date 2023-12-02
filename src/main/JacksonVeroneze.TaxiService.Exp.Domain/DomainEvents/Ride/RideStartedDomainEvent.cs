using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideStartedDomainEvent : BaseDomainEvent
{
    public RideStartedDomainEvent(Guid aggregateId) :
        base(aggregateId, "started")
    {
    }
}
