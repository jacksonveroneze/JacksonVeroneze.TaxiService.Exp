using JacksonVeroneze.NET.Cache.Interfaces;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Parameters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.State;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class StateDistribCachedRepository : IStateDistribCachedRepository
{
    private const string PrefixKey = "_distrib_cache_state_";

    private readonly ICacheService _cacheService;
    private readonly IStateRepository _repository;

    private readonly TimeSpan _cacheExpiration;

    public StateDistribCachedRepository(
        ICacheService cacheService,
        IStateRepository repository,
        StateParameters parameters)
    {
        _cacheService = cacheService;
        _repository = repository;

        _cacheService.WithPrefixKey(PrefixKey);

        _cacheExpiration = TimeSpan.FromMilliseconds(
            parameters.CacheExpMilisegundos);
    }

    public Task<ICollection<StateResult>?> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        const string key = "all";

        return _cacheService
            .GetOrCreateAsync(key, async entry =>
            {
                ICollection<StateResult>? result = await _repository
                    .GetAllAsync(cancellationToken);

                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                return result;
            }, cancellationToken);
    }

    public async Task<StateResult?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default)
    {
        ICollection<StateResult>? result =
            await GetAllAsync(cancellationToken);

        return result?.FirstOrDefault(item =>
            item.Id!.Equals(filter.Id, StringComparison.OrdinalIgnoreCase));
    }
}