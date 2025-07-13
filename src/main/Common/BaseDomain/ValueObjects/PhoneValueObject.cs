using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Errors;

namespace TaxiService.Common.BaseDomain.ValueObjects;

public class PhoneValueObject : ValueObject
{
    private const int MinLength = 10;
    private const int MaxLength = 15;

    public string? Value { get; }

    protected PhoneValueObject()
    {
    }

    public PhoneValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string?(
        PhoneValueObject? value)
        => value?.ToString();

    private static bool IsValid(string? value)
    {
        return !string.IsNullOrEmpty(value) &&
               value.Length is >= MinLength and <= MaxLength;
    }

    public override string ToString()
    {
        return Value ?? string.Empty;
    }

    public static Result<PhoneValueObject> Create(string? value)
    {
        if (!IsValid(value))
        {
            return Result<PhoneValueObject>.FromInvalid(
                CommonDomainErrors.PhoneError.InvalidPhone);
        }

        PhoneValueObject valueObject = new(value!);

        return Result<PhoneValueObject>.WithSuccess(valueObject);
    }
}
