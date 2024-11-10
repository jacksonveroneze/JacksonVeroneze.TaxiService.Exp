using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Results;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRidePagedQueryHandler(
    IMapper mapper,
    IRideReadRepository rideReadRepository)
    : IRequestHandler<GetRidePagedQuery, Result<GetRidePagedQueryResponse>>
{
    public async Task<Result<GetRidePagedQueryResponse>> Handle(
        GetRidePagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RidePagedFilter filter = mapper
            .Map<RidePagedFilter>(request);

        filter.CursorPagination = new CursorPaginationParameters()
        {
            Cursor = request.Cursor,
            Limit = request.PageSize
        };

        PaginatedResult<RideEntity> page = await rideReadRepository
            .GetCursorPagedAsync(filter, cancellationToken);

        GetRidePagedQueryResponse response =
            mapper.Map<GetRidePagedQueryResponse>(page);

        return Result<GetRidePagedQueryResponse>
            .WithSuccess(response);
    }
}