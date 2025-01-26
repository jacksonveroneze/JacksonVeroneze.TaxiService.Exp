using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace TaxiService.Common.Domain.DomainEvents.Base;

public class BaseDomainEvent(Guid aggregateId, string type)
    : DomainEvent(aggregateId);