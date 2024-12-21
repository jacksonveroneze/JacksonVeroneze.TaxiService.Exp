using JacksonVeroneze.TaxiService.Exp.Application.Models.Base.Pagination.Offset;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Email;

public sealed record GetEmailPagedQueryResponse
    : PagedResponse<EmailResponse>;