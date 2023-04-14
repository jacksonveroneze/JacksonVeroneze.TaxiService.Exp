using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.State;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class StatePaginatedRepository : IStatePaginatedRepository
{
    private readonly IStateDistribCachedRepository _repository;

    private readonly ICollection<StateResult> _empty = Enumerable
        .Empty<StateResult>()
        .ToArray();

    public StatePaginatedRepository(
        IStateDistribCachedRepository repository)
    {
        _repository = repository;
    }

    public async Task<Page<StateResult>> GetAllAsync(
        StateAllFilter filter,
        CancellationToken cancellationToken = default)
    {
        ICollection<StateResult>? result = await _repository
            .GetAllAsync(cancellationToken);

        result ??= _empty;

        return result.ToPageInMemory(filter.Pagination!);
    }
}