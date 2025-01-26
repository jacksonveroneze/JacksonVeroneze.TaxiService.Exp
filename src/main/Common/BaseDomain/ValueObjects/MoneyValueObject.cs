using System.Globalization;
using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.Domain.Core.Errors;

namespace TaxiService.Common.Domain.ValueObjects;

public class MoneyValueObject : ValueObject
{
    public decimal Value { get; }

    protected MoneyValueObject()
    {
    }

    public MoneyValueObject(decimal value)
    {
        Value = value;
    }

    public static implicit operator string?(MoneyValueObject? value)
        => value?.ToString();

    private static bool IsValid(decimal? value)
    {
        return !value.HasValue;
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }

    public static Result<MoneyValueObject> Create(decimal? value)
    {
        if (!IsValid(value))
        {
            return Result<MoneyValueObject>.FromInvalid(
                CommonDomainErrors.MoneyError.InvalidValue);
        }

        MoneyValueObject valueObject = new(value!.Value);

        return Result<MoneyValueObject>.WithSuccess(valueObject);
    }
}