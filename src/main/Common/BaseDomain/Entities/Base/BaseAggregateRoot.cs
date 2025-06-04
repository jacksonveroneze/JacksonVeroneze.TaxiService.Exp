using JacksonVeroneze.NET.DomainObjects.Domain;

namespace TaxiService.Common.BaseDomain.Entities.Base;

public abstract class BaseAggregateRoot : AggregateRoot
{
    public Guid TenantId { get; set; }

    protected BaseAggregateRoot() : base()
    {
    }
}
