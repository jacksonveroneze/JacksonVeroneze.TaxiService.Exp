using TaxiService.Modules.Passengers.Application.Models.Base.Pagination.Cursor;

namespace TaxiService.Modules.Passengers.Application.v1.Models.User;

public sealed record GetUserPagedQueryResponse
    : PagedResponse<UserResponse>;