namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class DistributedTracingConfiguration
{
    public bool IsEnabled { get; init; }

    public JaegerConfiguration? Jaeger { get; init; }
}

[ExcludeFromCodeCoverage]
public class JaegerConfiguration
{
    public string? Host { get; init; }

    public int? Port { get; init; }
}