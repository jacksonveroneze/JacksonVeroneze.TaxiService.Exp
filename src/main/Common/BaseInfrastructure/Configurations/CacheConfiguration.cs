namespace TaxiService.Common.BaseInfrastructure.Configurations;

[ExcludeFromCodeCoverage]
public record CacheConfiguration
{
    public CacheType Type { get; init; }

    public string? Endpoint { get; init; }
}

public enum CacheType
{
    Memory,
    Redis
}