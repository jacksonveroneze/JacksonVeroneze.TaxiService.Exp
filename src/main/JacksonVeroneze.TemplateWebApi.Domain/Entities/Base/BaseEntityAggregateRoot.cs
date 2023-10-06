using JacksonVeroneze.NET.DomainObjects.Domain;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;

public abstract class BaseEntityAggregateRoot : AggregateRoot<Guid>
{
    public Guid TenantId { get; set; }

    protected BaseEntityAggregateRoot() : base(Guid.NewGuid())
    {
    }
}
