using JacksonVeroneze.NET.DomainObjects.Domain;

namespace TaxiService.Common.BaseDomain.Entities.Base;

public abstract class BaseEntity : Entity
{
    public Guid TenantId { get; set; }

    protected BaseEntity() : base()
    {
    }
}
