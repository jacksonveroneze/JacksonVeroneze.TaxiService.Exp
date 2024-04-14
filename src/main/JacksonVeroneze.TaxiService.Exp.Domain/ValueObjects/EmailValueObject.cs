using System.Text.RegularExpressions;
using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

namespace JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

public class EmailValueObject : ValueObject
{
    private const int MaxLength = 256;

    private const string EmailRegexPattern =
        @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    private static readonly Lazy<Regex> EmailFormatRegex =
        new(() => new Regex(EmailRegexPattern,
            RegexOptions.Compiled | RegexOptions.IgnoreCase));

    public string? Value { get; }

    protected EmailValueObject()
    {
    }

    public EmailValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string?(EmailValueObject? value)
        => value?.ToString();

    private static bool IsValid(string? value)
    {
        return !string.IsNullOrEmpty(value) &&
               value.Length <= MaxLength &&
               EmailFormatRegex.Value.IsMatch(value);
    }

    public override string ToString()
    {
        return Value ?? string.Empty;
    }

    public static Result<EmailValueObject> Create(string? value)
    {
        if (!IsValid(value))
        {
            return Result<EmailValueObject>.FromInvalid(
                DomainErrors.EmailError.InvalidEmail);
        }

        EmailValueObject valueObject = new(value!);

        return Result<EmailValueObject>.WithSuccess(valueObject);
    }
}