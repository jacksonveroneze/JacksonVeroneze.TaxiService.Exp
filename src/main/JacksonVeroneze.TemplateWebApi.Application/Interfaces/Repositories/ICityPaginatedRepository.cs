using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface ICityPaginatedRepository
{
    Task<Page<CityEntity>> GetByStateIdPageAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default);
}
