using JacksonVeroneze.NET.Pagination.Enums;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Queries.Base;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;

namespace TaxiService.Modules.Passengers.Application.v1.Queries.Passenger.Email;

public sealed record GetEmailPagedQuery(Guid PassengerId) :
    PagedOffsetQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetEmailPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(PassengerResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;
}
