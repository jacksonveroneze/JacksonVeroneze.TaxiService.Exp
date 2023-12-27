using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideFinishedDomainEvent : BaseDomainEvent
{
    public RideFinishedDomainEvent(Guid aggregateId) :
        base(aggregateId, "finished")
    {
    }
}