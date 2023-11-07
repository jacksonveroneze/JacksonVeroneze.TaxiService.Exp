using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

[ExcludeFromCodeCoverage]
public class UserActivatedDomainEvent : BaseDomainEvent
{
    public UserActivatedDomainEvent(Guid aggregateId) : base(aggregateId, "activated")
    {
    }
}
