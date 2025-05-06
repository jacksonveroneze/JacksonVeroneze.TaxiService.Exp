using JacksonVeroneze.NET.Cache.Interfaces;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Drivers.Infrastructure.Repositories.Driver;

[ExcludeFromCodeCoverage]
public class DriverReadCacheRepository : IDriverReadCacheRepository
{
    private const string PrefixKey = "_distrib_cache_driver_";

    private readonly TimeSpan _cacheExpiration
        = TimeSpan.FromMinutes(5);

    private readonly ICacheService _cacheService;
    private readonly IDriverReadRepository _repository;

    public DriverReadCacheRepository(
        ICacheService cacheService,
        IDriverReadRepository repository)
    {
        _cacheService = cacheService;
        _repository = repository;

        _cacheService.WithPrefixKey(PrefixKey);
    }

    public Task<DriverDto?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        return _cacheService
            .GetOrCreateAsync(id.ToString(), async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                DriverDto? result = await _repository
                    .GetByIdAsync(id, cancellationToken);

                return result;
            }, cancellationToken);
    }

    public Task RemoveAsync(
        Guid id, CancellationToken cancellationToken)
    {
        return _cacheService.RemoveAsync(
            id.ToString(), cancellationToken);
    }
}