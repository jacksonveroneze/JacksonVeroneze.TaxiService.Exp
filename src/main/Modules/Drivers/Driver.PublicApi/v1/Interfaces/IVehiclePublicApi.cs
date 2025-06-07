using TaxiService.Modules.Driver.PublicApi.v1.Models;
using TaxiService.Modules.Driver.PublicApi.v1.Queries;

namespace TaxiService.Modules.Driver.PublicApi.v1.Interfaces;

public interface IVehiclePublicApi
{
    Task<ICollection<VehiclePublicApiDto>> GetAsync(
        VehiclePublicApiFilter filter,
        CancellationToken cancellationToken);
}
