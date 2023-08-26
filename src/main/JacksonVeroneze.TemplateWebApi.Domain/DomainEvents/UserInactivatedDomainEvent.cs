using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

public class UserInactivatedDomainEvent : DomainEvent
{
    public UserInactivatedDomainEvent(Guid aggregateId) : base(aggregateId)
    {
    }
}
