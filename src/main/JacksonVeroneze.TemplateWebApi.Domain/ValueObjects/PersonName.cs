namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public record PersonName
{
    public string? Value { get; }

    public PersonName(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        Value = value;
    }
}
