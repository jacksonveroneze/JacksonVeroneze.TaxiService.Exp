using JacksonVeroneze.NET.Pagination.Enums;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Queries;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Enums;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Application.v1.Driver.GetPaged;

public sealed record GetDriverPagedQuery() :
    PagedCursorQuery(DefaultOrderBy, DefaultOrder),
    IRequest<Result<GetDriverPagedQueryResponse>>
{
    private const string DefaultOrderBy = nameof(DriverResponse.FullName);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public string? Name { get; init; }

    public DriverStatus? Status { get; init; }
    
    public DriverExpands[]? Expand { get; init; }
}
