namespace TaxiService.Common.BaseInfrastructure.Configurations;

[ExcludeFromCodeCoverage]
public record AuthConfiguration
{
    public string? Authority { get; init; }

    public string? Audience { get; init; }

    public string? Issuer { get; init; }
}