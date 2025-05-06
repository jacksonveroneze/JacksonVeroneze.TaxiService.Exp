using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common;
using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle.Create;

public sealed record CreateCommandResponse
    : DataResponse<VehicleResponse>;
