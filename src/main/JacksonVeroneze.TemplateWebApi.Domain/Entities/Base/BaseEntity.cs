using JacksonVeroneze.NET.MongoDB.DomainObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;

public abstract class BaseEntity : BaseEntity<Guid>
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    // public Guid Id { get;} = Guid.NewGuid();
    //
    // public DateTime CreatedAt { get; init; } = DateTime.Now;
    //
    // public DateTime? UpdatedAt { get; set; }
    //
    // public DateTime? DeletedAt { get; set; }
    //
    // public int Version { get; set; } = 1;

    public override bool Equals(object? obj)
    {
        BaseEntity? compareTo = obj as BaseEntity;

        if (ReferenceEquals(this, compareTo)) return true;

        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(BaseEntity a, BaseEntity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(BaseEntity a, BaseEntity b)
        => !(a == b);

    public override int GetHashCode()
        => (GetType().GetHashCode() * 907) + Id.GetHashCode();

    public virtual bool IsValid()
        => throw new NotImplementedException();

    public override string ToString()
        => $"{GetType().Name} [Id={Id}]";
}
