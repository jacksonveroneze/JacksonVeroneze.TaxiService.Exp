using TaxiService.Common.BaseApplication.Models.Common;
using TaxiService.Modules.Passenger.Application.v1.Email.Common.Models;

namespace TaxiService.Modules.Passenger.Application.v1.Email.GetPaged;

public sealed record GetEmailPagedQueryResponse
    : CollectionResponse<EmailResponse>;
