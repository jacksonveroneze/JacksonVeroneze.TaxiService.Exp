using TaxiService.Common.BaseApplication.Models.Base;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Email;

public sealed record GetPagedQueryResponse
    : CollectionResponse<EmailResponse>;