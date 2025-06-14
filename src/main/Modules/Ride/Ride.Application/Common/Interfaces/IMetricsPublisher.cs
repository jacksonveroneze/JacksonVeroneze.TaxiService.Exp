namespace TaxiService.Modules.Ride.Application.Common.Interfaces;

public interface IMetricsPublisher
{
    void IncrementRequested();
    void IncrementAccepted();
    void IncrementStarted();
    void IncrementFinished();
    void IncrementCanceled();
}
