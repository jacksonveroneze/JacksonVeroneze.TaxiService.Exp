using TaxiService.Modules.Driver.Application.Common.Interfaces.Services;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Enums;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Driver.Application.Common.Services;

public class DriverExpandsService(
    IEnumerable<IDriverExpandsStrategy> expandStrategies)
    : IDriverExpandsService
{
    public Task ApplyAsync(
        DriverResponse driver,
        ICollection<DriverExpands>? expands,
        CancellationToken cancellationToken = default)
    {
        return ApplyExpandsAsync([driver],
            expands, cancellationToken);
    }

    public Task ApplyAsync(
        ICollection<DriverResponse> drivers,
        ICollection<DriverExpands>? expands,
        CancellationToken cancellationToken = default)
    {
        return ApplyExpandsAsync(drivers,
            expands, cancellationToken);
    }

    private async Task ApplyExpandsAsync(
        ICollection<DriverResponse> drivers,
        ICollection<DriverExpands>? expands,
        CancellationToken cancellationToken = default)
    {
        if (drivers.Count == 0 || expands is null || expands.Count == 0)
        {
            return;
        }

        IEnumerable<Task> expandsTasks = expandStrategies
            .Where(strategy =>
                expands.Contains(strategy.Name))
            .Select(strategy =>
                strategy.ApplyAsync(drivers, cancellationToken));

        await Task.WhenAll(expandsTasks);
    }
}
