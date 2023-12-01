using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.User;

public sealed class GetUserPagedQueryHandler(
    IMapper mapper,
    IUserReadRepository repository)
    : IRequestHandler<GetUserPagedQuery, IResult<GetUserPagedQueryResponse>>
{
    public async Task<IResult<GetUserPagedQueryResponse>> Handle(
        GetUserPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserPagedFilter filter = mapper
            .Map<UserPagedFilter>(request);

        Page<UserEntity> page = await repository
            .GetPagedAsync(filter, cancellationToken);

        GetUserPagedQueryResponse response =
            mapper.Map<GetUserPagedQueryResponse>(page);

        return Result<GetUserPagedQueryResponse>
            .Success(response);
    }
}
