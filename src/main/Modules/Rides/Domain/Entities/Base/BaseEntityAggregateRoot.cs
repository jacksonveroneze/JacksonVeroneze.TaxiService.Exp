using JacksonVeroneze.NET.DomainObjects.Domain;

namespace TaxiService.Modules.Rides.Domain.Entities.Base;

public abstract class BaseEntityAggregateRoot : AggregateRoot
{
    public Guid TenantId { get; set; }

    protected BaseEntityAggregateRoot() : base()
    {
    }
}