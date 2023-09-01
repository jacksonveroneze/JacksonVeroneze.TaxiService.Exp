using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.TemplateWebApi.Domain.Parameters;

[ExcludeFromCodeCoverage]
public sealed class MailParameters
{
    public const string Name = "Mail";

    public string? DisplayName { get; init; }

    public string? From { get; init; }

    public string? UserName { get; init; }

    public string? Password { get; init; }

    public string? Host { get; init; }

    public int Port { get; init; }

    public bool UseSsl { get; init; }

    public bool UseStartTls { get; init; }
}
