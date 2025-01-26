using JacksonVeroneze.NET.Pagination.Common;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Queries.Base;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;

public sealed record GetEmailPagedQuery(Guid UserId) :
    PagedOffsetQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetEmailPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(UserResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;
}