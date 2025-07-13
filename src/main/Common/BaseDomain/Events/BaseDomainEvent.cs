using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace TaxiService.Common.BaseDomain.Events;

public class BaseDomainEvent(Guid aggregateId, string type)
    : DomainEvent(aggregateId)
{
    public string Type { get; set; } = type;
}
