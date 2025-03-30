using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle.GetById;

public sealed record GetByIdQueryResponse
    : DataResponse<VehicleResponse>;