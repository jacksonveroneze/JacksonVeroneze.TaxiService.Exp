using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace TaxiService.Common.BaseDomain.DomainEvents.Base;

public class BaseDomainEvent(Guid aggregateId, string type)
    : DomainEvent(aggregateId);