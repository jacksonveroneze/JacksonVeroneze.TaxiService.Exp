using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

[ExcludeFromCodeCoverage]
public class UserInactivatedDomainEvent : BaseDomainEvent
{
    public UserInactivatedDomainEvent(Guid aggregateId) : base(aggregateId, "inactivated")
    {
    }
}
