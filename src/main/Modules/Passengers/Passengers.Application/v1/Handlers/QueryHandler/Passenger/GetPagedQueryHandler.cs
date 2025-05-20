using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passenger;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.Passenger;

public sealed class GetPagedQueryHandler(
    ILogger<GetPagedQueryHandler> logger,
    IDataMapper mapper,
    IPassengerReadRepository passengerReadRepository)
    : IRequestHandler<GetPagedQuery, Result<GetPagedQueryResponse>>
{
    public async Task<Result<GetPagedQueryResponse>> Handle(
        GetPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        PassengerPagedFilter filter = mapper
            .Map<PassengerPagedFilter>(request);

        Page<PassengerEntity> page = await passengerReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetPagedQueryResponse response =
            mapper.Map<GetPagedQueryResponse>(page);

        logger.LogGetPagedWithCursor(
            nameof(GetPagedQueryHandler),
            nameof(Handle), request.PageSize!.Value,
            page.Data.Count, page.PageInfo.HasMore!.Value);

        return Result<GetPagedQueryResponse>
            .WithSuccess(response);
    }
}
