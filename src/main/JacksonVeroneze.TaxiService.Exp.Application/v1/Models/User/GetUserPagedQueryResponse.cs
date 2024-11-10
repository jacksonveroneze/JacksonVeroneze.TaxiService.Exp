using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base.Pagination.CursorBased;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;

public sealed record GetUserPagedQueryResponse
    : PagedCursorResponse<UserResponse>;