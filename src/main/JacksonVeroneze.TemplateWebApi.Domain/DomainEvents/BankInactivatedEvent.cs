using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

public class BankInactivatedEvent : DomainEvent
{
    public BankInactivatedEvent(Guid aggregateId) : base(aggregateId)
    {
    }
}
