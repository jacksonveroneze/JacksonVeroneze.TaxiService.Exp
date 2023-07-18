using JacksonVeroneze.NET.HttpClient.Configuration;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class AppConfiguration
{
    private const string EnviromentDevelopment = "Development";
    private const string EnviromentProduction = "Production";

    public string? Environment { get; init; }

    public AppInfoConfiguration? Application { get; init; }

    public AppMetricsConfiguration? Metrics { get; init; }

    public SwaggerConfiguration? Swagger { get; init; }

    public CacheConfiguration? Cache { get; init; }

    public DistributedTracingConfiguration? DistributedTracing { get; init; }

    public ICollection<HttpClientConfiguration>? HttpClients { get; init; }

    public string AppName =>
        Application?.Name ?? string.Empty;

    public Version AppVersion =>
        Application?.Version!;

    public bool IsDevelopment =>
        Environment!.Equals(EnviromentDevelopment,
            StringComparison.OrdinalIgnoreCase);

    public bool IsProduction =>
        Environment!.Equals(EnviromentProduction,
            StringComparison.OrdinalIgnoreCase);
}
