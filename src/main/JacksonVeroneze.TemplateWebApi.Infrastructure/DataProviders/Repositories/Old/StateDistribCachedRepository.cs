using JacksonVeroneze.NET.Cache.Interfaces;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Parameters.Old;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Old;

public class StateDistribCachedRepository : IStateDistribCachedRepository
{
    private readonly ICollection<StateEntity> _empty = Enumerable
        .Empty<StateEntity>()
        .ToArray();

    private const string PrefixKey = "_distrib_cache_state_";

    private readonly ICacheService _cacheService;
    private readonly IStateRepository _repository;

    private readonly TimeSpan _cacheExpiration;

    public StateDistribCachedRepository(
        ICacheService cacheService,
        IStateRepository repository,
        StateParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        _cacheService = cacheService;
        _repository = repository;

        _cacheService.WithPrefixKey(PrefixKey);

        _cacheExpiration = TimeSpan.FromMilliseconds(
            parameters.CacheExpMilisegundos);
    }

    public async Task<ICollection<StateEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        const string key = "all";

        ICollection<StateEntity>? result = await _cacheService
            .GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                ICollection<StateEntity> result = await _repository
                    .GetAllAsync(cancellationToken);

                return result;
            }, cancellationToken);

        return result ?? _empty;
    }

    public async Task<StateEntity?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default)
    {
        ICollection<StateEntity> result =
            await GetAllAsync(cancellationToken);

        return result.FirstOrDefault(item =>
            item.Id!.Equals(filter.Id,
                StringComparison.OrdinalIgnoreCase));
    }
}
