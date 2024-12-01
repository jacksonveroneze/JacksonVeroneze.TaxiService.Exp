using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserInactivatedDomainEvent(Guid aggregateId)
    : BaseDomainEvent(aggregateId, "inactivated");