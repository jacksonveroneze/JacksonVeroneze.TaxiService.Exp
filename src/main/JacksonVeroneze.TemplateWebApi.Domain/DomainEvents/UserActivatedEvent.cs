using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

public class UserActivatedEvent : DomainEvent
{
    public UserActivatedEvent(Guid aggregateId) : base(aggregateId)
    {
    }
}
