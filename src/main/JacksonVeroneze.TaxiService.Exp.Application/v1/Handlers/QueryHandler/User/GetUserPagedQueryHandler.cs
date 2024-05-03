using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;

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
        int? a = null;

        ArgumentNullException.ThrowIfNull(a);

        UserPagedFilter filter = mapper
            .Map<UserPagedFilter>(request);

        Page<UserEntity> page = await userReadRepository
            .GetPagedAsync(filter, cancellationToken);

        GetUserPagedQueryResponse response =
            mapper.Map<GetUserPagedQueryResponse>(page);

        return Result<GetUserPagedQueryResponse>
            .WithSuccess(response);
    }
}