using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Passanger;

public sealed record CreateCommandResponse
    : DataResponse<PassangerResponse>;