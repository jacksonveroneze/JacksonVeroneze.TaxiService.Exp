using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideAcceptedDomainEvent : BaseDomainEvent
{
    public RideAcceptedDomainEvent(Guid aggregateId) :
        base(aggregateId, "accepted")
    {
    }
}
