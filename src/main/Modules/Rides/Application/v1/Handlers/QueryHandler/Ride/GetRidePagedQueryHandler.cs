using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.Application.Extensions;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Filters;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRidePagedQueryHandler(
    ILogger<GetRidePagedQueryHandler> logger,
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

        Page<RideEntity> page = await rideReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetRidePagedQueryResponse response =
            mapper.Map<GetRidePagedQueryResponse>(page);

        logger.LogGetPagedWithCursor(
            nameof(GetRidePagedQueryHandler),
            nameof(Handle), request.PageSize!.Value,
            page.Data.Count, page.PageInfo.HasMore);

        return Result<GetRidePagedQueryResponse>
            .WithSuccess(response);
    }
}