using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

public class BaseDomainEvent(Guid aggregateId, string type)
    : DomainEvent(aggregateId);