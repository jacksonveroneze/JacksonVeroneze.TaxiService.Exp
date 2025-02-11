using JacksonVeroneze.NET.Pagination.Common;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Queries.Base;
using TaxiService.Modules.Passengers.Application.v1.Models.Passanger;
using GetPagedQueryResponse = TaxiService.Modules.Passengers.Application.v1.Models.Email.GetPagedQueryResponse;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.Passanger.Email;

public sealed record GetPagedQuery(Guid PassangerId) :
    PagedOffsetQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(PassangerResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;
}