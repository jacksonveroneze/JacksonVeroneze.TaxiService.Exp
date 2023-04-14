using JacksonVeroneze.NET.Cache.Interfaces;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Parameters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.City;

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
        _cacheService = cacheService;
        _repository = repository;

        _cacheService.WithPrefixKey(PrefixKey);

        _cacheExpiration = TimeSpan.FromMilliseconds(
            parameters.CacheExpMilisegundos);
    }

    public Task<ICollection<CityResult>?> GetByStateIdAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default)
    {
        return _cacheService
            .GetOrCreateAsync(filter.StateId!, async entry =>
            {
                ICollection<CityResult>? result = await _repository
                    .GetByStateIdAsync(filter, cancellationToken);

                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                return result;
            }, cancellationToken);
    }
}