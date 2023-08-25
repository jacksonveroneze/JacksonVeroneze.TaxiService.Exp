using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.User;

internal sealed  class GetUserPagedQueryHandler :
    IRequestHandler<GetUserPagedQuery, IResult<BaseResponse>>
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

    public async Task<IResult<BaseResponse>> Handle(
        GetUserPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserPagedFilter filter = _mapper
            .Map<UserPagedFilter>(request);

        Page<UserEntity> data = await _repository
            .GetPagedAsync(filter, cancellationToken);

        GetUserPagedQueryResponse response =
            _mapper.Map<GetUserPagedQueryResponse>(data);

        _logger.LogGetPaged(nameof(GetUserPagedQueryHandler),
            nameof(Handle),
            data.Pagination.Page,
            data.Pagination.PageSize,
            data.Pagination.TotalElements);

        return Result<BaseResponse>.Success(response);
    }
}
