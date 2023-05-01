using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Pagination.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.City;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;

public class CityPaginatedRepository : ICityPaginatedRepository
{
    private readonly ICollection<CityResult> _empty = Enumerable
        .Empty<CityResult>()
        .ToArray();
    
    private readonly ICityDistribCachedRepository _repository;

    public CityPaginatedRepository(
        ICityDistribCachedRepository repository)
    {
        _repository = repository;
    }

    public async Task<Page<CityResult>> GetByStateIdPageAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default)
    {
        ICollection<CityResult>? result = await _repository
            .GetByStateIdAsync(filter, cancellationToken);

        result ??= _empty;

        return result.ToPageInMemory(filter.Pagination!);
    }
}