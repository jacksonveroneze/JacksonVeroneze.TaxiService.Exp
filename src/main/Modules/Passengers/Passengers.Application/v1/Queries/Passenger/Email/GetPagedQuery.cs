using JacksonVeroneze.NET.Pagination.Enums;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Queries.Base;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using GetPagedQueryResponse = TaxiService.Modules.Passengers.Application.v1.Models.Email.GetPagedQueryResponse;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.Passenger.Email;

public sealed record GetPagedQuery(Guid PassengerId) :
    PagedOffsetQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(PassengerResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;
}
