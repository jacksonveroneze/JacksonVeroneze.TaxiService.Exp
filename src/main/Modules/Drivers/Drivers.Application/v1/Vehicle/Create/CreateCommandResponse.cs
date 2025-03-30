using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle.Create;

public sealed record CreateCommandResponse
    : DataResponse<VehicleResponse>;