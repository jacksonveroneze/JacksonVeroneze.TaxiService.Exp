using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Results;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.QueryHandler.User;

public sealed class GetUserPagedQueryHandler(
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

        filter.CursorPagination = new CursorPaginationParameters()
        {
            Cursor = request.Cursor,
            Limit = request.PageSize
        };

        PaginatedResult<UserEntity> page = await userReadRepository
            .GetCursorPagedAsync(filter, cancellationToken);

        GetUserPagedQueryResponse response =
            mapper.Map<GetUserPagedQueryResponse>(page);

        return Result<GetUserPagedQueryResponse>
            .WithSuccess(response);
    }
}