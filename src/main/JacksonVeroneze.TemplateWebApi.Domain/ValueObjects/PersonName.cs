using JacksonVeroneze.NET.DomainObjects.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class PersonName : ValueObject
{
    public const int MaxLength = 100;

    public string? Value { get; }

    public PersonName(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        if (value.Length > MaxLength)
        {
            throw new ArgumentException();
        }

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
