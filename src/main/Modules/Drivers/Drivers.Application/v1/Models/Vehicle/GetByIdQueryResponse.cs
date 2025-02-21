using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Drivers.Application.v1.Models.Vehicle;

public sealed record GetByIdQueryResponse
    : DataResponse<VehicleResponse>;