namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;

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

    public AppMetricsConfiguration? Metrics { get; init; }

    public BusConfiguration? Bus { get; init; }

    public string AppName =>
        Application!.Name!;

    public Version AppVersion =>
        Application!.Version!;

    public bool IsDevelopment =>
        Environment!.Equals(EnviromentDevelopment,
            StringComparison.OrdinalIgnoreCase);

    public bool IsProduction =>
        Environment!.Equals(EnviromentProduction,
            StringComparison.OrdinalIgnoreCase);
}