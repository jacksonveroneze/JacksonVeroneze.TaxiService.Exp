using JacksonVeroneze.TaxiService.Exp.Application.Models.Base.Pagination.Cursor;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;

public sealed record GetUserPagedQueryResponse
    : PagedResponse<UserResponse>;