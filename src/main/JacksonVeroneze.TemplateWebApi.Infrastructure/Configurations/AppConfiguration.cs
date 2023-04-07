using JacksonVeroneze.NET.HttpClient.Configuration;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class AppConfiguration
{
    private const string EnviromentDevelopment = "Development";
    private const string EnviromentProduction = "Production";

    [Required]
    public string? Environment { get; set; }

    [Required]
    public AppInfoConfiguration? Application { get; set; }

    [Required]
    public SwaggerConfiguration? Swagger { get; set; }

    [Required]
    public DistributedTracingConfiguration? DistributedTracing { get; set; }

    public ICollection<HttpClientConfiguration>? HttpClients { get; set; }

    public CacheType CacheType { get; set; }
    public string? CacheEndpoint { get; set; }

    public string? AuthAuthority { get; set; }
    public string? AuthAudience { get; set; }

    public bool IsDevelopment =>
        Environment!.Equals(EnviromentDevelopment,
            StringComparison.OrdinalIgnoreCase);

    public bool IsProduction =>
        Environment!.Equals(EnviromentProduction,
            StringComparison.OrdinalIgnoreCase);
}