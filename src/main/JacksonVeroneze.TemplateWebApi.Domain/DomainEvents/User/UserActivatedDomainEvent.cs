using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserActivatedDomainEvent : BaseDomainEvent
{
    public UserActivatedDomainEvent(Guid aggregateId) :
        base(aggregateId, "activated")
    {
    }
}
