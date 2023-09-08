using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class PhoneValueObject : ValueObject
{
    public string? Value { get; private set; }

    protected PhoneValueObject()
    {
    }

    private PhoneValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string(PhoneValueObject? value)
        => value?.Value ?? string.Empty;

    public static IResult<PhoneValueObject> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result<PhoneValueObject>.Invalid(
                DomainErrors.Phone.InvalidPhone);
        }

        PhoneValueObject valueObject = new(value);

        return Result<PhoneValueObject>.Success(valueObject);
    }
}
