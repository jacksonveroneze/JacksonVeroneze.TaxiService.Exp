using JacksonVeroneze.TaxiService.Exp.Application.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Email;

public sealed record CreateEmailCommandResponse
    : DataResponse<EmailResponse>;