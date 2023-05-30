using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface ICityRepository
{
    Task<ICollection<CityEntity>> GetByStateIdAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default);
}