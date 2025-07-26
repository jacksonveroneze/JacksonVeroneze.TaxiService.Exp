using System.Collections.Concurrent;
using Prometheus;
using TaxiService.Common.SharedKernel.Interfaces;

namespace TaxiService.Common.BaseInfrastructure.Metrics;

public sealed class PrometheusMetricsPublisher : IMetricsPublisher
{
    private readonly ConcurrentDictionary<string, Counter> _counters =
        new(StringComparer.OrdinalIgnoreCase);

    public void Counter(string name, double increment = 1,
        IDictionary<string, string>? tags = null)
    {
        string[] labelNames = tags?.Keys.ToArray() ?? [];
        string key = GetKey(name, labelNames);

        Counter counter = _counters.GetOrAdd(key, _ =>
            Prometheus.Metrics.CreateCounter(name, $"auto-generated-{name}",
                new CounterConfiguration { LabelNames = labelNames }));

        counter.WithLabels(tags?.Values.ToArray() ?? []).Inc(increment);
    }

    private static string GetKey(string name, string[] labelNames)
    {
        return $"{name}:{string.Join(',', labelNames)}";
    }
}
