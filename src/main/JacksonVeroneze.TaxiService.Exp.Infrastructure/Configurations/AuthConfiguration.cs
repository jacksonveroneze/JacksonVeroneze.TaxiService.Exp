namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class AuthConfiguration
{
    public string? Authority { get; init; }

    public string? Audience { get; init; }

    public string? Issuer { get; init; }
}