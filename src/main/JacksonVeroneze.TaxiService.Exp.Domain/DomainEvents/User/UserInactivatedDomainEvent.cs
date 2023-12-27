using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserInactivatedDomainEvent : BaseDomainEvent
{
    public UserInactivatedDomainEvent(Guid aggregateId) :
        base(aggregateId, "inactivated")
    {
    }
}