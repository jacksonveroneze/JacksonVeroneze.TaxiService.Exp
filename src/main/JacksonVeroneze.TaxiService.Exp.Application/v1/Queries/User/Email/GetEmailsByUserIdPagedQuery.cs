using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;

public sealed record GetEmailsByUserIdPagedQuery(Guid UserId) :
    PagedQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetEmailsByUserIdPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(UserResponse.Name);

    private const SortDirection DefaultOrder = SortDirection.Ascending;
}