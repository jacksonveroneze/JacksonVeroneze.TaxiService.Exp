using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Models.State;
using JacksonVeroneze.TemplateWebApi.Application.Queries.State;
using JacksonVeroneze.TemplateWebApi.Domain.Results.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.State;

public class GetStatePagedQueryHandler : 
    IRequestHandler<GetStatePagedQuery, BaseResponse>
{
    private readonly ILogger<GetStatePagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IStateDistribCachedRepository _repository;

    public GetStatePagedQueryHandler(
        ILogger<GetStatePagedQueryHandler> logger,
        IMapper mapper,
        IStateDistribCachedRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<BaseResponse> Handle(
        GetStatePagedQuery request,
        CancellationToken cancellationToken)
    {
        ICollection<StateResult>? result =
            await _repository.GetAllAsync(cancellationToken);

        GetStatePagedQueryResponse response =
            _mapper.Map<GetStatePagedQueryResponse>(result);

        _logger.LogGetAllStates(nameof(GetStatePagedQueryHandler),
            nameof(Handle), result!.Count);

        return response;
    }
}