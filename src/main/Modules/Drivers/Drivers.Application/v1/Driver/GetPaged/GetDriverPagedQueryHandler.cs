using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Filters;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using IDriverReadRepository =
    TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.IDriverReadRepository;

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
            page.Data.Count, page.PageInfo.HasMore!.Value);

        return Result<GetDriverPagedQueryResponse>
            .WithSuccess(response);
    }
}
