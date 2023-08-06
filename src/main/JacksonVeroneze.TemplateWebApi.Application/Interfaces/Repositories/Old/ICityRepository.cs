using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Old;

public interface ICityRepository
{
    Task<ICollection<CityEntity>> GetByStateIdAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default);
}
