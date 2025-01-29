namespace TaxiService.Common.BaseInfrastructure.Configurations;

[ExcludeFromCodeCoverage]
public record DistributedTracingConfiguration
{
    public bool IsEnabled { get; init; }

    public DistributedTracingToolConfiguration? Jaeger { get; init; }
}

[ExcludeFromCodeCoverage]
public record DistributedTracingToolConfiguration
{
    public string? Host { get; init; }

    public int? Port { get; init; }
}