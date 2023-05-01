using System.Net;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
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
    private readonly IIbgeApi _ibgeApi;

    public CityRepository(ILogger<CityRepository> logger,
        IIbgeApi ibgeApi)
    {
        _logger = logger;
        _ibgeApi = ibgeApi;
    }

    public async Task<ICollection<CityResult>?> GetByStateIdAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ICollection<CityResult> result = await _ibgeApi
                .GetCitiesByStateAsync(filter.StateId!, cancellationToken);

            _logger.LogGetCitiesByStateId(nameof(CityRepository),
                nameof(GetByStateIdAsync), filter.StateId!, result.Count);

            return result;
        }
        catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            _logger.LogNotFound(nameof(CityRepository),
                nameof(GetByStateIdAsync), filter.StateId!, ex);

            return null;
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