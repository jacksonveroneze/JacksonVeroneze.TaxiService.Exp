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

    public CacheConfiguration? Cache { get; init; }

    public DatabaseConfiguration? Database { get; init; }

    public DistributedTracingConfiguration? DistributedTracing { get; init; }

    public ICollection<HttpClientConfiguration>? HttpClients { get; init; }

    public AppMetricsConfiguration? Metrics { get; init; }

    public PushGatewayConfiguration? PushGateway { get; init; }

    public string AppName =>
        Application?.Name ??
        throw new ArgumentNullException(nameof(AppName));

    public Version AppVersion =>
        Application?.Version ??
        throw new ArgumentNullException(nameof(AppVersion));

    public bool IsDevelopment =>
        Environment!.Equals(EnviromentDevelopment,
            StringComparison.OrdinalIgnoreCase);

    public bool IsProduction =>
        Environment!.Equals(EnviromentProduction,
            StringComparison.OrdinalIgnoreCase);
}
