namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class CacheConfiguration
{
    public CacheType Type { get; init; }

    public string? Endpoint { get; init; }
}

public enum CacheType
{
    Memory,
    Redis
}
