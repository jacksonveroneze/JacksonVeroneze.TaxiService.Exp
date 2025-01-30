using TaxiService.Common.BaseApplication.Models.Base;
using TaxiService.Common.BaseApplication.Models.Base.Pagination.Offset;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Email;

public sealed record GetEmailPagedQueryResponse
    : CollectionResponse<EmailResponse>;