using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class CpfValueObject : ValueObject
{
    private const int MinLength = 11;

    public string? Value { get; }

    private CpfValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string(CpfValueObject? value)
        => value?.Value ?? string.Empty;

    public static IResult<CpfValueObject> Create(string value)
    {
        if (string.IsNullOrEmpty(value) ||
            value.Length <= MinLength)
        {
            return Result<CpfValueObject>.Invalid(
                DomainErrors.User.InvalidCpf);
        }

        CpfValueObject valueObject = new(value);

        return Result<CpfValueObject>.Success(valueObject);
    }
}
