namespace TaxiService.Common.BaseInfrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record DatabaseConfiguration
{
    public string? ReadConnectionString { get; init; }
}