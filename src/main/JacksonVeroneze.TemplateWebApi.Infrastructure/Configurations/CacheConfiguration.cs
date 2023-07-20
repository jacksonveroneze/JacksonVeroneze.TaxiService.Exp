namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class CacheConfiguration
{
    public CacheType Type { get; init; }

    public string? Endpoint { get; init; }
}
