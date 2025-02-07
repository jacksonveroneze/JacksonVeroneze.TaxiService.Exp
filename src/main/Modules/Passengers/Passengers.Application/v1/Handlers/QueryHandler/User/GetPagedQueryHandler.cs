using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Application.v1.Queries.User;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.QueryHandler.User;

public sealed class GetPagedQueryHandler(
    ILogger<GetPagedQueryHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetPagedQuery, Result<GetPagedQueryResponse>>
{
    public async Task<Result<GetPagedQueryResponse>> Handle(
        GetPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserPagedFilter filter = mapper
            .Map<UserPagedFilter>(request);

        Page<UserEntity> page = await userReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetPagedQueryResponse response =
            mapper.Map<GetPagedQueryResponse>(page);

        logger.LogGetPagedWithCursor(
            nameof(GetPagedQueryHandler),
            nameof(Handle), request.PageSize!.Value,
            page.Data.Count, page.PageInfo.HasMore);

        return Result<GetPagedQueryResponse>
            .WithSuccess(response);
    }
}