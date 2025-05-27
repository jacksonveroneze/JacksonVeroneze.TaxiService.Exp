using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Email.Create;

public sealed record CreateEmailCommandResponse
    : DataResponse<EmailResponse>;
