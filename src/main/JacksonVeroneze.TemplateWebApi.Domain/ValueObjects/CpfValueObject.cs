using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class CpfValueObject : ValueObject
{
    private const int MinLength = 11;
    private const int MaxLength = 14;

    public string? Value { get; private set; }

    public string? ValueFormatted =>
        Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00");

    protected CpfValueObject()
    {
    }

    private CpfValueObject(string value)
    {
        Value = value
            .Replace(".", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("-", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("/", string.Empty, StringComparison.OrdinalIgnoreCase);
    }

    public static implicit operator string(CpfValueObject? value)
        => value?.Value ?? string.Empty;

    public static IResult<CpfValueObject> Create(string value)
    {
        if (string.IsNullOrEmpty(value) ||
            value.Length < MinLength ||
            value.Length > MaxLength)
        {
            return Result<CpfValueObject>.Invalid(
                DomainErrors.User.InvalidCpf);
        }

        CpfValueObject valueObject = new(value);

        return Result<CpfValueObject>.Success(valueObject);
    }
}
