using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface ICityRepository
{
    Task<ICollection<City>> GetByStateIdAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default);
}