using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class StatePaginatedRepository : IStatePaginatedRepository
{
    private readonly ICollection<StateEntity> _empty = Enumerable
        .Empty<StateEntity>()
        .ToArray();

    private readonly IStateDistribCachedRepository _repository;

    public StatePaginatedRepository(
        IStateDistribCachedRepository repository)
    {
        _repository = repository;
    }

    public async Task<Page<StateEntity>> GetAllAsync(
        StateAllFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        ICollection<StateEntity>? result = await _repository
            .GetAllAsync(cancellationToken);

        result ??= _empty;

        return result.ToPageInMemory(filter.Pagination!);
    }
}
