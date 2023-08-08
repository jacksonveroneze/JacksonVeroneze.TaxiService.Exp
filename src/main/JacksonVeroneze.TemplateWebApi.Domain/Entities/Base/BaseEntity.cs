namespace JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;

public abstract class BaseEntity
{
    public Guid Id { get; } = Guid.NewGuid();

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