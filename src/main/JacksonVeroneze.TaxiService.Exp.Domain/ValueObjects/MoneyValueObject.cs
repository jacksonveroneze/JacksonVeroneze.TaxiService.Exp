using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

namespace JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

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

    private static bool IsValid(decimal value)
    {
        return true;
    }

    public static Result<MoneyValueObject> Create(decimal value)
    {
        if (!IsValid(value))
        {
            return Result<MoneyValueObject>.FromInvalid(
                DomainErrors.Money.InvalidValue);
        }

        MoneyValueObject valueObject = new(value);

        return Result<MoneyValueObject>.WithSuccess(valueObject);
    }
}