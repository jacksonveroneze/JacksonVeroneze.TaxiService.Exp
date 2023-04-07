using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Models.State;
using JacksonVeroneze.TemplateWebApi.Application.Queries;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.State;

public class GetStateByIdQueryHandler : IRequestHandler<GetStateByIdQuery, BaseResponse>
{
    private readonly ILogger<GetStatePagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IStateDistribCachedRepository _repository;

    public GetStateByIdQueryHandler(
        ILogger<GetStatePagedQueryHandler> logger,
        IMapper mapper,
        IStateDistribCachedRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<BaseResponse> Handle(
        GetStateByIdQuery request,
        CancellationToken cancellationToken)
    {
        StateFilter filter = _mapper.Map<StateFilter>(request);

        StateResult? result = await _repository.GetByIdAsync(
            filter, cancellationToken);

        if (result is null)
        {
            _logger.LogNotFound(nameof(GetStateByIdQueryHandler),
                nameof(Handle));

            return new StateNotFoundResponse(request.Id!);
        }

        GetStateByIdQueryQueryResponse response =
            _mapper.Map<GetStateByIdQueryQueryResponse>(result);

        _logger.LogGetStateById(nameof(GetStateByIdQueryHandler),
            nameof(Handle), request.Id!);

        return response;
    }
}