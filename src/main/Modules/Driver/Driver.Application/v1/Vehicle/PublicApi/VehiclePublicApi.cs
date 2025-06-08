using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Driver.PublicApi.v1.Interfaces;
using TaxiService.Modules.Driver.PublicApi.v1.Models;
using TaxiService.Modules.Driver.PublicApi.v1.Queries;

namespace TaxiService.Modules.Driver.Application.v1.Vehicle.PublicApi;

public class VehiclePublicApi(
    IDataMapper mapper,
    IVehicleReadRepository repository)
    : IVehiclePublicApi
{
    public async Task<ICollection<VehiclePublicApiDto>> GetAsync(
        VehiclePublicApiFilter filter,
        CancellationToken cancellationToken)
    {
        VehicleFilter queryFilter = new()
        {
            DriverIds = filter.DriversIds,
        };

        ICollection<VehicleDto> data = await repository
            .GetAsync(queryFilter, cancellationToken);

        return mapper.Map<ICollection<VehiclePublicApiDto>>(data);
    }
}
