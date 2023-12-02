using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TaxiService.Exp.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserCreatedDomainEvent : BaseDomainEvent
{
    public UserCreatedDomainEvent(Guid aggregateId) :
        base(aggregateId, "created")
    {
    }
}
