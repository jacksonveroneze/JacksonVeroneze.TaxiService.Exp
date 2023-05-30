using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class CityPaginatedRepository : ICityPaginatedRepository
{
    private readonly ICollection<City> _empty = Enumerable
        .Empty<City>()
        .ToArray();

    private readonly ICityDistribCachedRepository _repository;

    public CityPaginatedRepository(
        ICityDistribCachedRepository repository)
    {
        _repository = repository;
    }

    public async Task<Page<City>> GetByStateIdPageAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default)
    {
        ICollection<City>? result = await _repository
            .GetByStateIdAsync(filter, cancellationToken);

        result ??= _empty;

        return result.ToPageInMemory(filter.Pagination!);
    }
}