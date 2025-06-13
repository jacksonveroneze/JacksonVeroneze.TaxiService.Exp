using TaxiService.Modules.Driver.Application.v1.Driver.Common.Enums;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Driver.Application.Common.Interfaces.Services;

public interface IDriverExpandsStrategy
{
    public DriverExpands Name { get; set; }

    Task ApplyAsync(
        ICollection<DriverResponse> drivers,
        CancellationToken cancellationToken = default);
}
