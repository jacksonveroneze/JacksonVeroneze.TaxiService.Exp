using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passanger;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.Passanger;

public sealed class GetPagedQueryHandler(
    ILogger<GetPagedQueryHandler> logger,
    IDataMapper mapper,
    IPassangerReadRepository passangerReadRepository)
    : IRequestHandler<GetPagedQuery, Result<GetPagedQueryResponse>>
{
    public async Task<Result<GetPagedQueryResponse>> Handle(
        GetPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        PassangerPagedFilter filter = mapper
            .Map<PassangerPagedFilter>(request);

        Page<PassangerEntity> page = await passangerReadRepository
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
