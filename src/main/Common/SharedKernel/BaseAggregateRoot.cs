using JacksonVeroneze.NET.DomainObjects.Domain;

namespace TaxiService.Common.SharedKernel;

public abstract class BaseAggregateRoot : AggregateRoot
{
    public Guid TenantId { get; set; }

    protected BaseAggregateRoot() : base()
    {
    }
}
