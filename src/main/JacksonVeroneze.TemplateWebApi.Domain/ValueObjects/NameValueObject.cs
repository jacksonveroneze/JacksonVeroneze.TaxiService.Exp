using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class NameValueObject : ValueObject
{
    public string? Value { get; }

    private NameValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string(NameValueObject? value)
        => value?.Value ?? string.Empty;

    public static IResult<NameValueObject> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result<NameValueObject>.Invalid(
                DomainErrors.User.InvalidName);
        }

        NameValueObject valueObject = new(value);

        return Result<NameValueObject>.Success(valueObject);
    }
}
