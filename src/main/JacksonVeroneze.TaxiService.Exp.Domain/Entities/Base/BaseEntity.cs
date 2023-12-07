using JacksonVeroneze.NET.DomainObjects.Domain;

namespace JacksonVeroneze.TaxiService.Exp.Domain.Entities.Base;

public abstract class BaseEntity : Entity<Guid>
{
    public Guid TenantId { get; set; }

    protected BaseEntity() : base(Guid.NewGuid())
    {
    }
}