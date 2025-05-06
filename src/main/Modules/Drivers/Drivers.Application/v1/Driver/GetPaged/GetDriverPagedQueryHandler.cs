using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Filters;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.GetPaged;

public sealed class GetDriverPagedQueryHandler(
    ILogger<GetDriverPagedQueryHandler> logger,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository)
    : IRequestHandler<GetDriverPagedQuery, Result<GetDriverPagedQueryResponse>>
{
    public async Task<Result<GetDriverPagedQueryResponse>> Handle(
        GetDriverPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        DriverPagedFilter filter = mapper
            .Map<DriverPagedFilter>(request);

        Page<DriverDto> page = await driverReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetDriverPagedQueryResponse response =
            mapper.Map<GetDriverPagedQueryResponse>(page);

        logger.LogGetPagedWithCursor(
            nameof(GetDriverPagedQueryResponse),
            nameof(Handle), request.PageSize!.Value,
            page.Data.Count, page.PageInfo.HasMore);

        return Result<GetDriverPagedQueryResponse>
            .WithSuccess(response);
    }
}
