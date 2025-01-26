using Prometheus.DotNetRuntime;
using TaxiService.Modules.Rides.Infrastructure.Configurations;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

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