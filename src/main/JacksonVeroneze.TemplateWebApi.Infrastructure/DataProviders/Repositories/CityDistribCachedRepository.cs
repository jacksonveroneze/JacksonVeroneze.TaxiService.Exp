using JacksonVeroneze.NET.Cache.Interfaces;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Parameters;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class CityDistribCachedRepository : ICityDistribCachedRepository
{
    private const string PrefixKey = "_distrib_cache_city_";

    private readonly ICacheService _cacheService;
    private readonly ICityRepository _repository;

    private readonly TimeSpan _cacheExpiration;

    public CityDistribCachedRepository(
        ICacheService cacheService,
        ICityRepository repository,
        CityParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        _cacheService = cacheService;
        _repository = repository;

        _cacheService.WithPrefixKey(PrefixKey);

        _cacheExpiration = TimeSpan.FromMilliseconds(
            parameters.CacheExpMilisegundos);
    }

    public Task<ICollection<CityEntity>> GetByStateIdAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        string key = filter.StateId!;

        return _cacheService
            .GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                ICollection<CityEntity> result = await _repository
                    .GetByStateIdAsync(filter, cancellationToken);

                return result;
            }, cancellationToken)!;
    }
}
