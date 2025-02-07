using TaxiService.Common.BaseApplication.Models.Base.Pagination.Cursor;

namespace TaxiService.Modules.Passengers.Application.v1.Models.User;

public sealed record GetPagedQueryResponse
    : PagedResponse<UserResponse>;