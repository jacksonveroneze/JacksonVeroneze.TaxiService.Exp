using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Rides.Application.Common.Interfaces.Services;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Filters;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Rides.Application.v1.Ride.GetPaged;

public sealed class GetRidePagedQueryHandler(
    ILogger<GetRidePagedQueryHandler> logger,
    IDataMapper mapper,
    IRideReadRepository rideReadRepository,
    IRideExpandsService rideExpandsService)
    : IRequestHandler<GetRidePagedQuery, Result<GetRidePagedQueryResponse>>
{
    public async Task<Result<GetRidePagedQueryResponse>> Handle(
        GetRidePagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RidePagedFilter filter = mapper
            .Map<RidePagedFilter>(request);

        Page<RideDto> page = await rideReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetRidePagedQueryResponse response =
            mapper.Map<GetRidePagedQueryResponse>(page);

        await rideExpandsService.ApplyAsync(
            response.Data!, request.Expand, cancellationToken);

        logger.LogGetPagedWithCursor(
            nameof(GetRidePagedQueryHandler),
            nameof(Handle), request.PageSize!.Value,
            page.Data.Count, page.PageInfo.HasMore!.Value);

        return Result<GetRidePagedQueryResponse>
            .WithSuccess(response);
    }
}
