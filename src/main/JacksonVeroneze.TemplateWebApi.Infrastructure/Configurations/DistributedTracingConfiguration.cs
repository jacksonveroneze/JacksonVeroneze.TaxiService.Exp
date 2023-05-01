namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class DistributedTracingConfiguration
{
    public bool IsEnabled { get; set; }

    public JaegerConfiguration? Jaeger { get; set; }
}

[ExcludeFromCodeCoverage]
public class JaegerConfiguration
{
    public string? Host { get; set; }

    public int? Port { get; set; }
}