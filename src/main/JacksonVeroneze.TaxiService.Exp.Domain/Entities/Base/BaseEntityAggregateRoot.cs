using JacksonVeroneze.NET.DomainObjects.Domain;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;

public abstract class BaseEntityAggregateRoot : AggregateRoot<Guid>
{
    public Guid TenantId { get; set; }

    protected BaseEntityAggregateRoot() : base(Guid.NewGuid())
    {
    }
}