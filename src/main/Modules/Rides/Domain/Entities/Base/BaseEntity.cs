using JacksonVeroneze.NET.DomainObjects.Domain;

namespace TaxiService.Modules.Rides.Domain.Entities.Base;

public abstract class BaseEntity : Entity
{
    public Guid TenantId { get; set; }

    protected BaseEntity() : base()
    {
    }
}