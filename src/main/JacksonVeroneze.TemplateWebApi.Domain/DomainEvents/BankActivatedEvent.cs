using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

public class BankActivatedEvent : DomainEvent
{
    public BankActivatedEvent(Guid aggregateId) : base(aggregateId)
    {
    }
}
