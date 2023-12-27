using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideCanceledDomainEvent : BaseDomainEvent
{
    public RideCanceledDomainEvent(Guid aggregateId) :
        base(aggregateId, "canceled")
    {
    }
}