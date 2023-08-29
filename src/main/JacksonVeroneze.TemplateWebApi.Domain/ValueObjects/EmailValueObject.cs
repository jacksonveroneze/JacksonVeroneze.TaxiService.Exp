using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class EmailValueObject : ValueObject
{
    public string? Value { get; }

    private EmailValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string(EmailValueObject value)
        => value?.Value ?? string.Empty;

    public static IResult<EmailValueObject> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result<EmailValueObject>.Invalid(
                DomainErrors.Email.InvalidEmail);
        }

        EmailValueObject valueObject = new(value);

        return Result<EmailValueObject>.Success(valueObject);
    }
}
