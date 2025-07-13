using Prometheus;
using TaxiService.Modules.Ride.Application.Common.Interfaces;

namespace TaxiService.Modules.Ride.Infrastructure;

public sealed class PrometheusMetricsPublisher : IMetricsPublisher
{
    public static class RideMetrics
    {
        public static readonly Counter Requested =
            Metrics.CreateCounter("ride_requested_total", "Total rides requested");

        public static readonly Counter Accepted =
            Metrics.CreateCounter("ride_accepted_total", "Total rides accepted");

        public static readonly Counter Started =
            Metrics.CreateCounter("ride_started_total", "Total rides started");

        public static readonly Counter Finished =
            Metrics.CreateCounter("ride_finished_total", "Total rides finished");

        public static readonly Counter Canceled =
            Metrics.CreateCounter("ride_canceled_total", "Total rides canceled");
    }


    public void IncrementRequested() => RideMetrics.Requested.Inc();
    public void IncrementAccepted() => RideMetrics.Accepted.Inc();
    public void IncrementStarted() => RideMetrics.Started.Inc();
    public void IncrementFinished() => RideMetrics.Finished.Inc();
    public void IncrementCanceled() => RideMetrics.Canceled.Inc();
}
