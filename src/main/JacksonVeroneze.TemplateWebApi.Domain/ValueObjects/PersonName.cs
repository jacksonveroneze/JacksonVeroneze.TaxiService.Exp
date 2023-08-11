namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public record PersonName
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
}
