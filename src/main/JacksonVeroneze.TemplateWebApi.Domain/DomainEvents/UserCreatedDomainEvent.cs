using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

[ExcludeFromCodeCoverage]
public class UserCreatedDomainEvent : DomainEvent
{
    public UserCreatedDomainEvent(Guid aggregateId) : base(aggregateId)
    {
    }
}
