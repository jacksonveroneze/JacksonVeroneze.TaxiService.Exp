using TaxiService.Common.Application.Models.Base.Pagination.Offset;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Email;

public sealed record GetEmailPagedQueryResponse
    : PagedResponse<EmailResponse>;