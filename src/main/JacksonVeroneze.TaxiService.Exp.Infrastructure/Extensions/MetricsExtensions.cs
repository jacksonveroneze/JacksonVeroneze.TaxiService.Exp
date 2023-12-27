using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using Prometheus.DotNetRuntime;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MetricsExtensions
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