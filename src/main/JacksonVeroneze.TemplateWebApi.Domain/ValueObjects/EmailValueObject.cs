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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }

    public static implicit operator string(EmailValueObject password)
        => password?.Value ?? string.Empty;

    public static IResult<EmailValueObject> Create(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        // TODO - Validações

        if (string.IsNullOrEmpty(value))
        {
            return Result<EmailValueObject>.Invalid(
                DomainErrors.User.InvalidEmail);
        }

        EmailValueObject valueObject = new(value);

        return Result<EmailValueObject>.Success(valueObject);
    }
}
