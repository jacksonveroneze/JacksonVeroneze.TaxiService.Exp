using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Driver.Application.v1.Driver.GetById;

public sealed record GetDriverByIdQueryResponse
    : DataResponse<DriverResponse>;
