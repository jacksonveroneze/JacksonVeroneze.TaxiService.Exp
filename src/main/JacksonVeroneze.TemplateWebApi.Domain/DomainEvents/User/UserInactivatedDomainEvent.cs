using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserInactivatedDomainEvent : BaseDomainEvent
{
    public UserInactivatedDomainEvent(Guid aggregateId) :
        base(aggregateId, "inactivated")
    {
    }
}
