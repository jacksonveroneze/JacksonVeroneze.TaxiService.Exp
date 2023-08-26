using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.User;

internal sealed  class GetUserPagedQueryHandler :
    IRequestHandler<GetUserPagedQuery, IResult<GetUserPagedQueryResponse>>
{
    private readonly ILogger<GetUserPagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserReadRepository _repository;

    public GetUserPagedQueryHandler(
        ILogger<GetUserPagedQueryHandler> logger,
        IMapper mapper,
        IUserReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<GetUserPagedQueryResponse>> Handle(
        GetUserPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserPagedFilter filter = _mapper
            .Map<UserPagedFilter>(request);

        Page<UserEntity> page = await _repository
            .GetPagedAsync(filter, cancellationToken);

        GetUserPagedQueryResponse response =
            _mapper.Map<GetUserPagedQueryResponse>(page);

        _logger.LogGetPaged(nameof(GetUserPagedQueryHandler),
            nameof(Handle),
            page.Pagination.Page,
            page.Pagination.PageSize,
            page.Pagination.TotalElements);

        return Result<GetUserPagedQueryResponse>.Success(response);
    }
}
