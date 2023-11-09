using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Ride;

[ExcludeFromCodeCoverage]
public class RideFinishedDomainEvent : BaseDomainEvent
{
    public RideFinishedDomainEvent(Guid aggregateId) :
        base(aggregateId, "finished")
    {
    }
}
