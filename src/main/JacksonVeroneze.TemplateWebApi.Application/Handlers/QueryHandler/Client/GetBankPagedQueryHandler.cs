using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.Client;

internal sealed  class GetClientPagedQueryHandler :
    IRequestHandler<GetClientPagedQuery, IResult<BaseResponse>>
{
    private readonly ILogger<GetClientPagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IClientReadRepository _repository;

    public GetClientPagedQueryHandler(
        ILogger<GetClientPagedQueryHandler> logger,
        IMapper mapper,
        IClientReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<BaseResponse>> Handle(
        GetClientPagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ClientPagedFilter filter = _mapper
            .Map<ClientPagedFilter>(request);

        Page<ClientEntity> data = await _repository
            .GetPagedAsync(filter, cancellationToken);

        GetClientPagedQueryResponse response =
            _mapper.Map<GetClientPagedQueryResponse>(data);

        _logger.LogGetPaged(nameof(GetClientPagedQueryHandler),
            nameof(Handle),
            data.Pagination.Page,
            data.Pagination.PageSize,
            data.Pagination.TotalElements);

        return Result<BaseResponse>.Success(response);
    }
}
