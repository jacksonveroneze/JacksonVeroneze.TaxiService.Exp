using TaxiService.Common.Application.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Email;

public sealed record CreateEmailCommandResponse
    : DataResponse<EmailResponse>;