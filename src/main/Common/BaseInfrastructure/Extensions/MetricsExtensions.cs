using Prometheus.DotNetRuntime;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MetricsExtensions
{
    public static IDisposable? AddMetrics(
        AppConfiguration? appConfiguration)
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
}