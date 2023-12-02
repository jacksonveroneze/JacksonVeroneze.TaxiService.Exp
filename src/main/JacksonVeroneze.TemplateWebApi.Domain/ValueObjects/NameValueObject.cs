using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class NameValueObject : ValueObject
{
    private const int MinLength = 2;
    private const int MaxLength = 100;

    public string? Value { get; private set; }

    protected NameValueObject()
    {
    }

    public NameValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string(NameValueObject? value)
        => value?.Value ?? string.Empty;

    public static Result<NameValueObject> Create(string value)
    {
        if (string.IsNullOrEmpty(value) ||
            value.Length <= MinLength ||
            value.Length > MaxLength)
        {
            return Result<NameValueObject>.FromInvalid(
                DomainErrors.User.InvalidName);
        }

        NameValueObject valueObject = new(value);

        return Result<NameValueObject>.WithSuccess(valueObject);
    }
}
