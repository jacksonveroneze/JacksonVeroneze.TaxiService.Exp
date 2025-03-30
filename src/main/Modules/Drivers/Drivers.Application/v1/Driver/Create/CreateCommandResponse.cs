using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Create;

public sealed record CreateCommandResponse
    : DataResponse<DriverResponse>;