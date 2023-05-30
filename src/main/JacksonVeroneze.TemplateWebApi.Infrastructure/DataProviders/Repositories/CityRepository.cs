using AutoMapper;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.City;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.HttpClients;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using Refit;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class CityRepository : ICityRepository
{
    private readonly ILogger<CityRepository> _logger;
    private readonly IMapper _mapper;
    private readonly IIbgeApi _ibgeApi;

    public CityRepository(ILogger<CityRepository> logger,
        IMapper mapper,
        IIbgeApi ibgeApi)
    {
        _logger = logger;
        _mapper = mapper;
        _ibgeApi = ibgeApi;
    }

    public async Task<ICollection<CityEntity>> GetByStateIdAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ICollection<CityResult> result = await _ibgeApi
                .GetCitiesByStateAsync(filter.StateId!, cancellationToken);

            _logger.LogGetCitiesByStateId(nameof(CityRepository),
                nameof(GetByStateIdAsync), filter.StateId!, result.Count);

            return _mapper.Map<ICollection<CityEntity>>(result);
        }
        catch (ApiException ex)
        {
            _logger.LogGenericHttpError(nameof(CityRepository),
                nameof(GetByStateIdAsync), ex.StatusCode, ex);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogGenericError(nameof(CityRepository),
                nameof(GetByStateIdAsync), ex);

            throw;
        }
    }
}