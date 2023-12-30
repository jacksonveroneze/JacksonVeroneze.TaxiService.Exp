using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;

public sealed record GetEmailsByUserIdPagedQueryResponse : PagedResponse<EmailResponse>;