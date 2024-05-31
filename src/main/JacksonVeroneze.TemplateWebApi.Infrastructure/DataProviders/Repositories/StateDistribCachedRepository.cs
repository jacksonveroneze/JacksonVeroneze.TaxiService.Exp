using JacksonVeroneze.NET.Cache.Interfaces;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Parameters;
using RedLockNet;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class StateDistribCachedRepository : IStateDistribCachedRepository
{
    private static readonly SemaphoreSlim SemaphoreSlim = new(5);

    private const string PrefixKey = "_distrib_cache_state_";

    private readonly ICacheService _cacheService;
    private readonly IDistributedLockFactory _distributedLockFactory;
    private readonly IStateRepository _repository;
    private readonly StateParameters _parameters;

    private readonly TimeSpan _cacheExpiration;

    public StateDistribCachedRepository(
        ICacheService cacheService,
        IDistributedLockFactory distributedLockFactory,
        IStateRepository repository,
        StateParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        _cacheService = cacheService;
        _distributedLockFactory = distributedLockFactory;
        _repository = repository;
        _parameters = parameters;

        _cacheService.WithPrefixKey(PrefixKey);

        _cacheExpiration = TimeSpan.FromMilliseconds(
            parameters.CacheExpMilisegundos);
    }

    public Task<ICollection<StateEntity>?> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return _parameters.Source switch
        {
            "D" => GetAllDistributedLockAsync(cancellationToken),
            "S" => GetAllSemaphoreAsync(cancellationToken),
            _ => GetAllDirectAsync(cancellationToken)
        };
    }

    public async Task<StateEntity?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default)
    {
        ICollection<StateEntity>? result =
            await GetAllAsync(cancellationToken)
                .ConfigureAwait(false);

        return result?.FirstOrDefault(item =>
            item.Id!.Equals(filter.Id,
                StringComparison.OrdinalIgnoreCase));
    }

    private async Task<ICollection<StateEntity>?> GetAllDistributedLockAsync(
        CancellationToken cancellationToken = default)
    {
        const string key = "all";

        ICollection<StateEntity>? resultCached = await _cacheService
            .GetAsync<ICollection<StateEntity>?>(key, cancellationToken)
            .ConfigureAwait(false);

        if (resultCached != null)
        {
            return resultCached;
        }

        TimeSpan expiryTime = TimeSpan.FromSeconds(2); // Lock expira após 5 segundos
        TimeSpan waitTime = TimeSpan.FromSeconds(5); // Tempo máximo de espera para adquirir o lock
        TimeSpan retryDelay = TimeSpan.FromSeconds(1); // Intervalo de 2 segundos entre tentativas

        await using IRedLock? distributedLock =
            await _distributedLockFactory.CreateLockAsync(
                PrefixKey, expiryTime, waitTime, retryDelay
            ).ConfigureAwait(false);

        if (!distributedLock.IsAcquired)
        {
            throw new InvalidOperationException(
                "Resource is locked right now. Try again later!");
        }

        return await _cacheService
            .GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                ICollection<StateEntity>? result = await _repository
                    .GetAllAsync(cancellationToken);

                return result;
            }, cancellationToken).ConfigureAwait(false);
    }

    private async Task<ICollection<StateEntity>?> GetAllSemaphoreAsync(
        CancellationToken cancellationToken = default)
    {
        const string key = "all";

        ICollection<StateEntity>? resultCached = await _cacheService
            .GetAsync<ICollection<StateEntity>?>(key, cancellationToken)
            .ConfigureAwait(false);

        if (resultCached != null)
        {
            return resultCached;
        }

        await SemaphoreSlim.WaitAsync(cancellationToken)
            .ConfigureAwait(false);

        try
        {
            return await _cacheService
                .GetOrCreateAsync(key, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                    ICollection<StateEntity>? result = await _repository
                        .GetAllAsync(cancellationToken);

                    return result;
                }, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            SemaphoreSlim.Release();
        }
    }

    private async Task<ICollection<StateEntity>?> GetAllDirectAsync(
        CancellationToken cancellationToken = default)
    {
        const string key = "all";

        return await _cacheService
            .GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheExpiration;

                ICollection<StateEntity>? result = await _repository
                    .GetAllAsync(cancellationToken);

                return result;
            }, cancellationToken).ConfigureAwait(false);
    }
}
