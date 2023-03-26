namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class AppConfiguration
{
    private const string EnviromentDevelopment = "Development";
    private const string EnviromentProduction = "Production";

    public string? Environment { get; set; }

    public string? AuthAuthority { get; set; }
    public string? AuthAudience { get; set; }

    public AppInfoConfiguration? Application { get; set; }

    public SwaggerConfiguration? Swagger { get; set; }

    public DistributedTracingConfiguration? DistributedTracing { get; set; }

    public bool IsDevelopment =>
        Environment!.Equals(EnviromentDevelopment,
            StringComparison.OrdinalIgnoreCase);

    public bool IsProduction =>
        Environment!.Equals(EnviromentProduction,
            StringComparison.OrdinalIgnoreCase);
}