using TaxiService.Modules.Driver.Application.v1.Driver.Common.Enums;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Driver.Application.Common.Interfaces.Services;

public interface IDriverExpandsService
{
    Task ApplyAsync(
        DriverResponse driver,
        ICollection<DriverExpands>? expands,
        CancellationToken cancellationToken = default);

    Task ApplyAsync(
        ICollection<DriverResponse> drivers,
        ICollection<DriverExpands>? expands,
        CancellationToken cancellationToken = default);
}
