using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace TaxiService.Modules.Rides.Domain.DomainEvents.Base;

public class BaseDomainEvent(Guid aggregateId, string type)
    : DomainEvent(aggregateId);