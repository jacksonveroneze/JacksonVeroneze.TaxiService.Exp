using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

[ExcludeFromCodeCoverage]
public class UserDeletedDomainEvent : BaseDomainEvent
{
    public UserDeletedDomainEvent(Guid aggregateId) : base(aggregateId, "deleted")
    {
    }
}
