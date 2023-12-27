using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Validators;

namespace JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

public class CpfValueObject : ValueObject
{
    private const int Length = 11;
    private const string MaskFormat = @"000\.000\.000\-00";

    public string? Value { get; }

    public string ValueFormatted =>
        Convert.ToUInt64(Value).ToString(MaskFormat);

    protected CpfValueObject()
    {
    }

    private CpfValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string?(CpfValueObject? value)
        => value?.ToString();

    private static bool IsValid(string? value)
    {
        return !string.IsNullOrEmpty(value) &&
               value.Length == Length &&
               CpfValidator.Validate(value);
    }

    public override string ToString()
    {
        return Value ?? string.Empty;
    }

    public static Result<CpfValueObject> Create(string? value)
    {
        if (!IsValid(value))
        {
            return Result<CpfValueObject>.FromInvalid(
                DomainErrors.UserError.InvalidCpf);
        }

        CpfValueObject valueObject = new(value!);

        return Result<CpfValueObject>
            .WithSuccess(valueObject);
    }
}