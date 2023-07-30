using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Models.State;
using JacksonVeroneze.TemplateWebApi.Application.Queries.State;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.State;

public class GetStatePagedQueryHandler :
    IRequestHandler<GetStatePagedQuery, BaseResponse>
{
    private readonly ILogger<GetStatePagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IStatePaginatedRepository _repository;

    public GetStatePagedQueryHandler(
        ILogger<GetStatePagedQueryHandler> logger,
        IMapper mapper,
        IStatePaginatedRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<BaseResponse> Handle(
        GetStatePagedQuery request,
        CancellationToken cancellationToken)
    {
        StateAllFilter filter = _mapper.Map<StateAllFilter>(request);

        Page<StateEntity> result =
            await _repository.GetAllAsync(filter, cancellationToken);

        GetStatePagedQueryResponse response =
            _mapper.Map<GetStatePagedQueryResponse>(result);

        _logger.LogGetAllStates(nameof(GetStatePagedQueryHandler),
            nameof(Handle), result.Pagination.TotalElements);

        return response;
    }
}
