using AutoMapper;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
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
    private readonly IMapper _mapper;
    private readonly IIbgeApi _ibgeApi;

    public StateRepository(ILogger<StateRepository> logger,
        IMapper mapper,
        IIbgeApi ibgeApi)
    {
        _logger = logger;
        _mapper = mapper;
        _ibgeApi = ibgeApi;
    }

    public async Task<ICollection<StateEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            ICollection<StateResult> result = await _ibgeApi
                .GetStatesAsync(cancellationToken);

            _logger.LogGetAllStates(nameof(StateRepository),
                nameof(GetAllAsync), result.Count);

            return _mapper.Map<ICollection<StateEntity>>(result);
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

    public Task<StateEntity?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}