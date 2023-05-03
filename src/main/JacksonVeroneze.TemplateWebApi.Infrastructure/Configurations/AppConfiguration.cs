using JacksonVeroneze.NET.HttpClient.Configuration;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class AppConfiguration
{
    private const string EnviromentDevelopment = "Development";
    private const string EnviromentProduction = "Production";

    public string? Environment { get; init; }

    public AppInfoConfiguration? Application { get; init; }

    public SwaggerConfiguration? Swagger { get; init; }

    public DistributedTracingConfiguration? DistributedTracing { get; init; }

    public ICollection<HttpClientConfiguration>? HttpClients { get; init; }

    public CacheType CacheType { get; init; }
    public string? CacheEndpoint { get; init; }

    public string? AuthAuthority { get; init; }
    public string? AuthAudience { get; init; }

    public string AppName =>
        Application?.Name ?? String.Empty;

    public string AppVersion =>
        Application?.Version ?? String.Empty;

    public bool IsDevelopment =>
        Environment!.Equals(EnviromentDevelopment,
            StringComparison.OrdinalIgnoreCase);

    public bool IsProduction =>
        Environment!.Equals(EnviromentProduction,
            StringComparison.OrdinalIgnoreCase);
}