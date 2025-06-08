using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Models;

namespace TaxiService.Modules.Driver.Application.v1.Vehicle.GetById;

public sealed record GetVehicleByIdQueryResponse
    : DataResponse<VehicleResponse>;
