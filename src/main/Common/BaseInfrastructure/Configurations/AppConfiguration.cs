using JacksonVeroneze.NET.HttpClient.Configuration;

namespace TaxiService.Common.BaseInfrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppConfiguration
{
    private const string EnviromentDevelopment = "Development";
    private const string EnviromentProduction = "Production";

    public string? Environment { get; init; }

    public AppInfoConfiguration? Application { get; init; }

    public CacheConfiguration? Cache { get; init; }

    public DatabaseConfiguration? Database { get; init; }

    public DistributedTracingConfiguration? DistributedTracing { get; init; }

    public BusConfiguration? Bus { get; init; }

    public AuthConfiguration? Auth { get; init; }

    public ICollection<HttpClientConfiguration>? HttpClients { get; init; }
    
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