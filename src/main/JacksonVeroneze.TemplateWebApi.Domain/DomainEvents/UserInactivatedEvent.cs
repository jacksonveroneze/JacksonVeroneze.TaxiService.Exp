using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

public class UserInactivatedEvent : DomainEvent
{
    public UserInactivatedEvent(Guid aggregateId) : base(aggregateId)
    {
    }
}
