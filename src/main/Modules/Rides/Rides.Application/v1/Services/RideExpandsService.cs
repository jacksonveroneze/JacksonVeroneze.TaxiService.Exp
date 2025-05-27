using TaxiService.Modules.Rides.Application.v1.Interfaces.Services;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;

namespace TaxiService.Modules.Rides.Application.v1.Services;

public class RideExpandsService(
    IEnumerable<IRideExpandsStrategy> expandStrategies)
    : IRideExpandsService
{
    public Task ApplyAsync(
        RideResponse ride,
        ICollection<RideExpands>? expands,
        CancellationToken cancellationToken = default)
    {
        return ApplyExpandsAsync([ride],
            expands, cancellationToken);
    }

    public Task ApplyAsync(
        ICollection<RideResponse> rides,
        ICollection<RideExpands>? expands,
        CancellationToken cancellationToken = default)
    {
        return ApplyExpandsAsync(rides,
            expands, cancellationToken);
    }

    private async Task ApplyExpandsAsync(
        ICollection<RideResponse> rides,
        ICollection<RideExpands>? expands,
        CancellationToken cancellationToken = default)
    {
        if (rides.Count == 0 || expands is null || expands.Count == 0)
        {
            return;
        }

        IEnumerable<Task> expandsTasks = expandStrategies
            .Where(strategy =>
                expands.Contains(strategy.Name))
            .Select(strategy =>
                strategy.ApplyAsync(rides, cancellationToken));

        await Task.WhenAll(expandsTasks);
    }
}
