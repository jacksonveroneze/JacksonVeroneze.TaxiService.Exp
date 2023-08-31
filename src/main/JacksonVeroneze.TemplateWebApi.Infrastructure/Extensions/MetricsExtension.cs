using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using Prometheus.DotNetRuntime;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MetricsExtension
{
    public static IDisposable? AddMetrics(
        AppConfiguration? appConfiguration)
    {
        if (appConfiguration?.Metrics?.Detailed ?? false)
        {
            return DotNetRuntimeStatsBuilder
                .Customize()
                .WithContentionStats(CaptureLevel.Informational)
                .WithJitStats(CaptureLevel.Informational)
                .WithThreadPoolStats(CaptureLevel.Informational)
                .WithGcStats(CaptureLevel.Informational)
                .WithExceptionStats()
                .StartCollecting();
        }

        return null;
    }
}
