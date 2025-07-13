using TaxiService.Common.BaseApplication.Models.Common;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Driver.Application.v1.Driver.Create;

public sealed record CreateDriverCommandResponse
    : DataResponse<DriverResponse>;
