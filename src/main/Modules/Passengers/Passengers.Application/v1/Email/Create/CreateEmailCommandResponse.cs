using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Modules.Passengers.Application.v1.Email.Common.Models;

namespace TaxiService.Modules.Passengers.Application.v1.Email.Create;

public sealed record CreateEmailCommandResponse
    : DataResponse<EmailResponse>;
