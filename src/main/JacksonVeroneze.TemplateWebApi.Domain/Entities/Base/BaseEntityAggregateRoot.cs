using JacksonVeroneze.NET.DomainObjects.Domain;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;

public abstract class BaseEntityAggregateRoot : AggregateRoot<Guid>
{
    protected BaseEntityAggregateRoot() : base(Guid.NewGuid())
    {
    }
}
