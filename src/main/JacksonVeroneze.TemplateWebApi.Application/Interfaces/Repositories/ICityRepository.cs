using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.City;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface ICityRepository
{
    Task<ICollection<CityResult>?> GetByStateIdAsync(CityFilter filter,
        CancellationToken cancellationToken = default);
}