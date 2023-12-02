using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

namespace JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

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

    public static Result<PhoneValueObject> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result<PhoneValueObject>.FromInvalid(
                DomainErrors.Phone.InvalidPhone);
        }

        PhoneValueObject valueObject = new(value);

        return Result<PhoneValueObject>.WithSuccess(valueObject);
    }
}
