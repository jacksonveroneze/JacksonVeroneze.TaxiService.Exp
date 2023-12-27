namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class AppInfoConfiguration
{
    public string? Name { get; init; }

    public string? Description { get; init; }

    public Version? Version { get; init; }
}