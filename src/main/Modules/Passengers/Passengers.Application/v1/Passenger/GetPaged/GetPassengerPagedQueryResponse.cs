using TaxiService.Common.BaseApplication.Models.Base.Pagination.Cursor;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Models;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.GetPaged;

public sealed record GetPassengerPagedQueryResponse
    : PagedResponse<PassengerResponse>;
