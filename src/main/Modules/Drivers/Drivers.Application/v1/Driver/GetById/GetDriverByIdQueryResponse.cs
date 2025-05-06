using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.GetById;

public sealed record GetDriverByIdQueryResponse
    : DataResponse<DriverResponse>;
