using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Email.GetPaged;

public sealed record GetEmailPagedQueryResponse
    : CollectionResponse<EmailResponse>;
