using JacksonVeroneze.NET.DomainObjects.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class PhoneValueObject : ValueObject
{
    public string? Value { get; }

    public PhoneValueObject(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
