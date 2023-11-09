using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideStartedDomainEvent : BaseDomainEvent
{
    public RideStartedDomainEvent(Guid aggregateId) :
        base(aggregateId, "started")
    {
    }
}
