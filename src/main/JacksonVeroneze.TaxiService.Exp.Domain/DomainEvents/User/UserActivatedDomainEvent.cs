using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserActivatedDomainEvent : BaseDomainEvent
{
    public UserActivatedDomainEvent(Guid aggregateId) :
        base(aggregateId, "activated")
    {
    }
}