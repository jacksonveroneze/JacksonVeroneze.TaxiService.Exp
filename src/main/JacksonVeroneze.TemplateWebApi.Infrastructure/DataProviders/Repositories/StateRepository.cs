using System.Net;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.State;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.HttpClients;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using Refit;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class StateRepository : IStateRepository
{
    private readonly ILogger<StateRepository> _logger;
    private readonly IIbgeApi _ibgeApi;

    public StateRepository(ILogger<StateRepository> logger,
        IIbgeApi ibgeApi)
    {
        _logger = logger;
        _ibgeApi = ibgeApi;
    }

    public async Task<ICollection<StateResult>?> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            ICollection<StateResult> result = await _ibgeApi
                .GetStatesAsync(cancellationToken);

            _logger.LogGetAllStates(nameof(StateRepository),
                nameof(GetAllAsync), result.Count);

            return result;
        }
        catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.NotFound)
        {
            _logger.LogNotFound(nameof(CityRepository),
                nameof(GetAllAsync), string.Empty, ex);

            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogGenericHttpError(nameof(StateRepository),
                nameof(GetAllAsync), ex.StatusCode, ex);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogGenericError(nameof(StateRepository),
                nameof(GetAllAsync), ex);

            throw;
        }
    }

    public Task<StateResult?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}