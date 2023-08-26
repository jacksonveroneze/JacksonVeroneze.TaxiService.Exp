using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

public class UserActivatedDomainEvent : DomainEvent
{
    public UserActivatedDomainEvent(Guid aggregateId) : base(aggregateId)
    {
    }
}
