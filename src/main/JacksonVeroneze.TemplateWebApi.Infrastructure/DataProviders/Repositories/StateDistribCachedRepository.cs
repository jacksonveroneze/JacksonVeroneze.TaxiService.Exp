using JacksonVeroneze.NET.Cache.Interfaces;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Parameters;

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

    public Task<ICollection<State>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        const string key = "all";

        return _cacheService
            .GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                ICollection<State> result = await _repository
                    .GetAllAsync(cancellationToken);

                return result;
            }, cancellationToken)!;
    }

    public async Task<State?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default)
    {
        ICollection<State> result =
            await GetAllAsync(cancellationToken);

        return result.FirstOrDefault(item =>
            item.Id!.Equals(filter.Id,
                StringComparison.OrdinalIgnoreCase));
    }
}