using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Old;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Models.Old.State;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Old.State;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;

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
        ArgumentNullException.ThrowIfNull(request);

        StateByIdFilter filter = _mapper.Map<StateByIdFilter>(request);

        StateEntity? result = await _repository.GetByIdAsync(
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
