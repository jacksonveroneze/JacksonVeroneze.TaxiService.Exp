using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.GetById;

public sealed record GetByIdQueryResponse
    : DataResponse<DriverResponse>;