using JacksonVeroneze.NET.DomainObjects.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class NameValueObject : ValueObject
{
    public string? Value { get; }

    public NameValueObject(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
