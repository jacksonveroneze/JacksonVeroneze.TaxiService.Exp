using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

[ExcludeFromCodeCoverage]
public class UserCreatedDomainEvent : BaseDomainEvent
{
    public UserCreatedDomainEvent(Guid aggregateId) : base(aggregateId, "created")
    {
    }
}
