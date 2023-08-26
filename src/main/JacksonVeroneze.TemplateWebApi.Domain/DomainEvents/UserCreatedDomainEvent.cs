using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

public class UserCreatedDomainEvent : DomainEvent
{
    public UserCreatedDomainEvent(Guid aggregateId) : base(aggregateId)
    {
    }
}
