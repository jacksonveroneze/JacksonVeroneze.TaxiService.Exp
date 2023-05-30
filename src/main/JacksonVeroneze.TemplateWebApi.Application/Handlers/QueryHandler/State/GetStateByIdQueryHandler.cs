using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Models.State;
using JacksonVeroneze.TemplateWebApi.Application.Queries.State;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.State;

public class GetStateByIdQueryHandler :
    IRequestHandler<GetStateByIdQuery, BaseResponse>
{
    private readonly ILogger<GetStateByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IStateDistribCachedRepository _repository;

    public GetStateByIdQueryHandler(
        ILogger<GetStateByIdQueryHandler> logger,
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
        StateByIdFilter filter = _mapper.Map<StateByIdFilter>(request);

        Domain.Entities.StateEntity? result = await _repository.GetByIdAsync(
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