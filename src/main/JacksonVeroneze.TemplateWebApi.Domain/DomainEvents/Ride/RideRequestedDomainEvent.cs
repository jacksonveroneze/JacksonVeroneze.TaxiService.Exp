using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideRequestedDomainEvent : BaseDomainEvent
{
    public RideRequestedDomainEvent(Guid aggregateId) :
        base(aggregateId, "requested")
    {
    }
}
