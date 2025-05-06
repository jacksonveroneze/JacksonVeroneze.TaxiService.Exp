using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Create;

public sealed record CreateDriverCommandResponse
    : DataResponse<DriverResponse>;
