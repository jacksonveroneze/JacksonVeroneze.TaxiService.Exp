using TaxiService.Common.BaseApplication.Models.Base.Pagination.Cursor;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.GetPaged;

public sealed record GetPassengerPagedQueryResponse
    : PagedResponse<PassengerResponse>;
