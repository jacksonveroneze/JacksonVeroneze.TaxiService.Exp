using JacksonVeroneze.NET.DomainObjects.Domain;

namespace TaxiService.Common.Domain.Entities.Base;

public abstract class BaseEntity : Entity
{
    public Guid TenantId { get; set; }

    protected BaseEntity() : base()
    {
    }
}