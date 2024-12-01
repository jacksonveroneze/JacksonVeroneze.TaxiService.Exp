using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserActivatedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "activated");