using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.DomainObjects.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;

[ExcludeFromCodeCoverage]
public class UserActivatedDomainEvent : DomainEvent
{
    public UserActivatedDomainEvent(Guid aggregateId) : base(aggregateId)
    {
    }
}
