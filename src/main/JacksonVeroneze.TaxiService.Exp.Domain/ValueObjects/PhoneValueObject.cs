using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

namespace JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

public class PhoneValueObject : ValueObject
{
    public string? Value { get; }

    protected PhoneValueObject()
    {
    }

    private PhoneValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string(PhoneValueObject? value)
        => value?.Value ?? string.Empty;

    private static bool IsValid(string value)
    {
        return !string.IsNullOrEmpty(value);
    }

    public static Result<PhoneValueObject> Create(string value)
    {
        if (!IsValid(value))
        {
            return Result<PhoneValueObject>.FromInvalid(
                DomainErrors.Phone.InvalidPhone);
        }

        PhoneValueObject valueObject = new(value);

        return Result<PhoneValueObject>.WithSuccess(valueObject);
    }
}
