using JacksonVeroneze.NET.Pagination.Cursor;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.QueryHandler.User;

public sealed class GetUserPagedQueryHandler(
    ILogger<GetUserPagedQueryHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetUserPagedQuery, Result<GetUserPagedQueryResponse>>
{
    public async Task<Result<GetUserPagedQueryResponse>> Handle(
        GetUserPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserPagedFilter filter = mapper
            .Map<UserPagedFilter>(request);

        Page<UserEntity> page = await userReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetUserPagedQueryResponse response =
            mapper.Map<GetUserPagedQueryResponse>(page);

        logger.LogGetPagedWithCursor(
            nameof(GetUserPagedQueryHandler),
            nameof(Handle), request.PageSize!.Value,
            page.Data.Count, page.PageInfo.HasMore);

        return Result<GetUserPagedQueryResponse>
            .WithSuccess(response);
    }
}