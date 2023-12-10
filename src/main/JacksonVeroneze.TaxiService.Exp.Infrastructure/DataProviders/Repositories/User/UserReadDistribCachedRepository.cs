using JacksonVeroneze.NET.Cache.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Parameters;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.User;

public class UserReadDistribCachedRepository : IUserReadDistribCachedRepository
{
    private const string PrefixKey = "_distrib_cache_user_";

    private readonly ICacheService _cacheService;
    private readonly IUserReadRepository _repository;

    private readonly TimeSpan _cacheExpiration;

    public UserReadDistribCachedRepository(
        ICacheService cacheService,
        IUserReadRepository repository,
        UserParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        _cacheService = cacheService;
        _repository = repository;

        _cacheService.WithPrefixKey(PrefixKey);

        _cacheExpiration = TimeSpan.FromMilliseconds(
            parameters.CacheExpMilisegundos);
    }

    public Task<bool> ExistsByEmailAsync(
        string email, CancellationToken
            cancellationToken = default)
    {
        string key = email;

        return _cacheService
            .GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                bool result = await _repository
                    .ExistsByEmailAsync(email, cancellationToken);

                return result;
            }, cancellationToken);
    }

    public Task<UserEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        string key = id.ToString();

        return _cacheService
            .GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                UserEntity? result = await _repository
                    .GetByIdAsync(id, cancellationToken);

                return result;
            }, cancellationToken);
    }
}
