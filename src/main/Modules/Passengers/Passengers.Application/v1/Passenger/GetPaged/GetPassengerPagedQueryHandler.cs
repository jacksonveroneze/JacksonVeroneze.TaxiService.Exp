using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Models;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.GetPaged;

public sealed class GetPassengerPagedQueryHandler(
    ILogger<GetPassengerPagedQueryHandler> logger,
    IDataMapper mapper,
    IPassengerReadRepository passengerReadRepository)
    : IRequestHandler<GetPassengerPagedQuery, Result<GetPassengerPagedQueryResponse>>
{
    public async Task<Result<GetPassengerPagedQueryResponse>> Handle(
        GetPassengerPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        PassengerPagedFilter filter = mapper
            .Map<PassengerPagedFilter>(request);

        Page<PassengerDto> page = await passengerReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetPassengerPagedQueryResponse response =
            mapper.Map<GetPassengerPagedQueryResponse>(page);

        logger.LogGetPagedWithCursor(
            nameof(GetPassengerPagedQueryHandler),
            nameof(Handle), request.PageSize!.Value,
            page.Data.Count, page.PageInfo.HasMore!.Value);

        return Result<GetPassengerPagedQueryResponse>
            .WithSuccess(response);
    }
}
