using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserCreatedDomainEvent : BaseDomainEvent
{
    public UserCreatedDomainEvent(Guid aggregateId) :
        base(aggregateId, "created")
    {
    }
}
