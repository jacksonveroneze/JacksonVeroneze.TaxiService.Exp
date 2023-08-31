using System.Text.RegularExpressions;
using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public class EmailValueObject : ValueObject
{
    private const int MaxLength = 256;

    private const string EmailRegexPattern =
        @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    private static readonly Lazy<Regex> EmailFormatRegex =
        new(() => new Regex(EmailRegexPattern,
            RegexOptions.Compiled | RegexOptions.IgnoreCase));

    public string? Value { get; }

    private EmailValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string(EmailValueObject? value)
        => value?.Value ?? string.Empty;

    public static IResult<EmailValueObject> Create(string value)
    {
        if (string.IsNullOrEmpty(value) ||
            value.Length > MaxLength ||
            !EmailFormatRegex.Value.IsMatch(value))
        {
            return Result<EmailValueObject>.Invalid(
                DomainErrors.Email.InvalidEmail);
        }

        EmailValueObject valueObject = new(value);

        return Result<EmailValueObject>.Success(valueObject);
    }
}
