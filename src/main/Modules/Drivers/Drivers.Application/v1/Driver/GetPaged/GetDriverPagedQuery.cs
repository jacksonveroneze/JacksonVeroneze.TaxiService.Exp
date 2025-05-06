using JacksonVeroneze.NET.Pagination.Common;
using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Queries.Base;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.GetPaged;

public sealed record GetDriverPagedQuery() :
    PagedCursorQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetDriverPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(DriverResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public string? Name { get; init; }

    public DriverStatus? Status { get; init; }
}
