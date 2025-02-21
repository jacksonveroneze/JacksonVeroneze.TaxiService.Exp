using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Drivers.Application.v1.Models.Driver;

public sealed record GetByIdQueryResponse
    : DataResponse<DriverResponse>;