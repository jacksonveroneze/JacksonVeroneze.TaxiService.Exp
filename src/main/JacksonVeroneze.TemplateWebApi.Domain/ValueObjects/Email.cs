using System.Text.RegularExpressions;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

public record Email
{
    public const int MaxLength = 256;

    private const string EmailRegexPattern =
        @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    private static readonly Lazy<Regex> EmailFormatRegex =
        new Lazy<Regex>(() => new Regex(EmailRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    public string? Value { get; }

    public Email(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        if (!Regex.IsMatch(value, EmailRegexPattern))
        {
            throw new ArgumentException();
        }

        Value = value;
    }
}
