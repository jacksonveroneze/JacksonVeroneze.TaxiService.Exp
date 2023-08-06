namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public record Email
{
    public string? Value { get; }

    public Email(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        Value = value;
    }
}
