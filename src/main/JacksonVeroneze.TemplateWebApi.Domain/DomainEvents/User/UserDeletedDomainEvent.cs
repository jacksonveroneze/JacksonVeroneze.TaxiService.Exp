using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.User;

[ExcludeFromCodeCoverage]
public class UserDeletedDomainEvent : BaseDomainEvent
{
    public UserDeletedDomainEvent(Guid aggregateId) :
        base(aggregateId, "deleted")
    {
    }
}
