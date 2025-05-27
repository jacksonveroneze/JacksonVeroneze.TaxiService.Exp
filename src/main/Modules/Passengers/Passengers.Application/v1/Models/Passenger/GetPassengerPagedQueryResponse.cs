using TaxiService.Common.BaseApplication.Models.Base.Pagination.Cursor;

namespace TaxiService.Modules.Passengers.Application.v1.Models.Passenger;

public sealed record GetPassengerPagedQueryResponse
    : PagedResponse<PassengerResponse>;
