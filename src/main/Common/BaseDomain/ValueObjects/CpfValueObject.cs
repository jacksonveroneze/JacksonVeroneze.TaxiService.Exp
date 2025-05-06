using System.Globalization;

using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseDomain.Core.Errors;
using TaxiService.Common.BaseDomain.Validators;

namespace TaxiService.Common.BaseDomain.ValueObjects;

public class CpfValueObject : ValueObject
{
    private const int Length = 11;
    private const string MaskFormat = @"000\.000\.000\-00";

    public string? Value { get; }

    public string ValueFormatted =>
        Convert.ToUInt64(Value, CultureInfo.InvariantCulture).ToString(MaskFormat);

    protected CpfValueObject()
    {
    }

    public CpfValueObject(string value)
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
                CommonDomainErrors.CpfError.InvalidCpf);
        }

        CpfValueObject valueObject = new(value!);

        return Result<CpfValueObject>
            .WithSuccess(valueObject);
    }
}
