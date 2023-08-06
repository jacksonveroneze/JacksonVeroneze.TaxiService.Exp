using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Old;

public class StatePaginatedRepository : IStatePaginatedRepository
{
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

        ICollection<StateEntity> result = await _repository
            .GetAllAsync(cancellationToken);

        return result.ToPageInMemory(filter.Pagination!);
    }
}
