using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

namespace JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

public class NameValueObject : ValueObject
{
    private const int MinLength = 2;
    private const int MaxLength = 100;

    public string? Value { get; }

    protected NameValueObject()
    {
    }

    public NameValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string?(NameValueObject? value)
        => value?.ToString();

    private static bool IsValid(string? value)
    {
        return !string.IsNullOrEmpty(value) &&
               value.Length >= MinLength &&
               value.Length <= MaxLength;
    }

    public override string ToString()
    {
        return Value ?? string.Empty;
    }

    public static Result<NameValueObject> Create(string? value)
    {
        if (!IsValid(value))
        {
            return Result<NameValueObject>.FromInvalid(
                DomainErrors.UserError.InvalidName);
        }

        NameValueObject valueObject = new(value!);

        return Result<NameValueObject>.WithSuccess(valueObject);
    }
}