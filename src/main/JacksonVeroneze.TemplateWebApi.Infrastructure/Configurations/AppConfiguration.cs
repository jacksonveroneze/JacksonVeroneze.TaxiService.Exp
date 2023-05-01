using JacksonVeroneze.NET.HttpClient.Configuration;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class AppConfiguration
{
    private const string EnviromentDevelopment = "Development";
    private const string EnviromentProduction = "Production";

    public string? Environment { get; set; }

    public AppInfoConfiguration? Application { get; set; }

    public SwaggerConfiguration? Swagger { get; set; }

    public DistributedTracingConfiguration? DistributedTracing { get; set; }

    public ICollection<HttpClientConfiguration>? HttpClients { get; set; }

    public CacheType CacheType { get; set; }
    public string? CacheEndpoint { get; set; }

    public string? AuthAuthority { get; set; }
    public string? AuthAudience { get; set; }

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