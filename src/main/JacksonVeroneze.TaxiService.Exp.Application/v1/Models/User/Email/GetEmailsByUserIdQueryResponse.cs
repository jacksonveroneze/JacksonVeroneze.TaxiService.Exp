using JacksonVeroneze.TaxiService.Exp.Application.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;

public sealed record GetEmailsByUserIdQueryResponse
    : CollectionResponse<EmailResponse>;