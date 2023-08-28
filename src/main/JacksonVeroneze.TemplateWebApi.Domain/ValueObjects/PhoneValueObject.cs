using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class PhoneValueObject : ValueObject
{
    public string? Value { get; }

    private PhoneValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string(PhoneValueObject value)
        => value?.Value ?? string.Empty;

    public static IResult<PhoneValueObject> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result<PhoneValueObject>.Invalid(
                DomainErrors.User.InvalidPhone);
        }

        PhoneValueObject valueObject = new(value);

        return Result<PhoneValueObject>.Success(valueObject);
    }
}
