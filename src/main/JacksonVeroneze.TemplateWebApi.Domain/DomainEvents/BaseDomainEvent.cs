using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

public class BaseDomainEvent : DomainEvent
{
    public string Type { get; set; }

    public BaseDomainEvent(Guid aggregateId, string type) :
        base(aggregateId)
    {
        Type = type;
    }
}
