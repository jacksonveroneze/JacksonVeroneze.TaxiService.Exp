using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Old;

public class CityPaginatedRepository : ICityPaginatedRepository
{
    private readonly ICityDistribCachedRepository _repository;

    public CityPaginatedRepository(
        ICityDistribCachedRepository repository)
    {
        _repository = repository;
    }

    public async Task<Page<CityEntity>> GetByStateIdPageAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        ICollection<CityEntity> result = await _repository
            .GetByStateIdAsync(filter, cancellationToken);

        return result.ToPageInMemory(filter.Pagination!);
    }
}
