using JacksonVeroneze.NET.DomainObjects.Domain;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;

public abstract class BaseEntity : Entity<Guid>
{
    protected BaseEntity() : base(Guid.NewGuid())
    {
    }
}
