using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Old;

public interface ICityPaginatedRepository
{
    Task<Page<CityEntity>> GetByStateIdPageAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default);
}
