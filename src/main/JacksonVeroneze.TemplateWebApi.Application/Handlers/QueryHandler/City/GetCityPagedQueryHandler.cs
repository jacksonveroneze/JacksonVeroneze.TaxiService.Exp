using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Models.City;
using JacksonVeroneze.TemplateWebApi.Application.Models.State;
using JacksonVeroneze.TemplateWebApi.Application.Queries.City;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.City;
using JacksonVeroneze.TemplateWebApi.Domain.Results.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.City;

public class GetCityPagedQueryHandler :
    IRequestHandler<GetCityByStatePagedQuery, BaseResponse>
{
    private readonly ILogger<GetCityPagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IStateDistribCachedRepository _stateRepository;
    private readonly ICityPaginatedRepository _cityRepository;

    public GetCityPagedQueryHandler(
        ILogger<GetCityPagedQueryHandler> logger,
        IMapper mapper,
        IStateDistribCachedRepository stateRepository,
        ICityPaginatedRepository cityRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _stateRepository = stateRepository;
        _cityRepository = cityRepository;
    }

    public async Task<BaseResponse> Handle(
        GetCityByStatePagedQuery request,
        CancellationToken cancellationToken)
    {
        StateByIdFilter filter = _mapper.Map<StateByIdFilter>(request);

        Domain.Entities.State? result = await _stateRepository.GetByIdAsync(
            filter, cancellationToken);

        if (result is null)
        {
            _logger.LogNotFound(nameof(GetCityPagedQueryHandler),
                nameof(Handle));

            return new StateNotFoundResponse(request.StateId!);
        }

        CityByStateFilter cityFilter = _mapper.Map<CityByStateFilter>(request);

        Page<Domain.Entities.City> page = await _cityRepository
            .GetByStateIdPageAsync(cityFilter, cancellationToken);

        GetCityByStatePagedQueryResponse response =
            _mapper.Map<GetCityByStatePagedQueryResponse>(page);

        _logger.LogGetCityByState(nameof(GetCityPagedQueryHandler),
            nameof(Handle), request.StateId!);

        return response;
    }
}