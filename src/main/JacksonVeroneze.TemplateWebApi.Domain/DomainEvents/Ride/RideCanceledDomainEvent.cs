using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideCanceledDomainEvent : BaseDomainEvent
{
    public RideCanceledDomainEvent(Guid aggregateId) :
        base(aggregateId, "canceled")
    {
    }
}
