namespace TaxiService.Common.BaseInfrastructure.Configurations;

[ExcludeFromCodeCoverage]
public record DatabaseConfiguration
{
    public string? ReadConnectionString { get; init; }
}