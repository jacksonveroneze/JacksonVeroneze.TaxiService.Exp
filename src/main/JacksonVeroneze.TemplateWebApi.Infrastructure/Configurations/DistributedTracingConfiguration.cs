namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class DistributedTracingConfiguration
{
    public bool IsEnabled { get; init; }

    public DistributedTracingToolConfiguration? Jaeger { get; init; }
}

[ExcludeFromCodeCoverage]
public class DistributedTracingToolConfiguration
{
    public string? Host { get; init; }

    public int? Port { get; init; }
}
